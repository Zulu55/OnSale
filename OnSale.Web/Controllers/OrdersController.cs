using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnSale.Common.Enums;
using OnSale.Web.Data;
using OnSale.Web.Data.Entities;
using OnSale.Web.Helpers;
using OnSale.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnSale.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;

        public OrdersController(DataContext context, ICombosHelper combosHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Orders
                .Include(p => p.User)
                .Include(p => p.OrderDetails)
                .ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order order = await _context.Orders
                .Include(o => o.User)
                .ThenInclude(u => u.City)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .ThenInclude(od => od.Category)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .ThenInclude(od => od.ProductImages)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            ChangeOrderStatusViewModel model = new ChangeOrderStatusViewModel
            {
                Date = DateTime.Now,
                Id = order.Id,
                OrderStatuses = _combosHelper.GetOrderStatuses(),
                OrderStatusId = (int)order.OrderStatus
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ChangeOrderStatusViewModel model)
        {
            if (ModelState.IsValid)
            {
                Order order = await _context.Orders.FindAsync(model.Id);
                order.OrderStatus = ToOrderStatus(model.OrderStatusId);
                if (model.OrderStatusId == 2) // sent
                {
                    order.DateSent = model.Date.ToUniversalTime();
                }
                else if (model.OrderStatusId == 3) // confirmed
                {
                    order.DateConfirmed = model.Date.ToUniversalTime();
                }

                _context.Update(order);
                await _context.SaveChangesAsync();
                return RedirectToAction($"{nameof(Details)}/{model.Id}");
            }

            model.OrderStatuses = _combosHelper.GetOrderStatuses();
            return View(model);
        }

        private OrderStatus ToOrderStatus(int orderStatusId)
        {
            switch (orderStatusId)
            {
                case 0: return OrderStatus.Pending;
                case 1: return OrderStatus.Spreading;
                case 2: return OrderStatus.Sent;
                case 3: return OrderStatus.Confirmed;
                default: return OrderStatus.Cancelled;
            }
        }
    }
}
