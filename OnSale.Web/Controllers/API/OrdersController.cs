using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnSale.Common.Enums;
using OnSale.Common.Responses;
using OnSale.Web.Data;
using OnSale.Web.Data.Entities;
using OnSale.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnSale.Web.Controllers.API
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public OrdersController(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        [HttpPost]
        public async Task<IActionResult> PostOrder([FromBody] OrderResponse request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            string email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                return NotFound("Error001");
            }

            Order order = new Order
            {
                Date = DateTime.UtcNow,
                OrderDetails = new List<OrderDetail>(),
                OrderStatus = OrderStatus.Pending,
                PaymentMethod = request.PaymentMethod,
                Remarks = request.Remarks,
                User = user
            };

            foreach (OrderDetailResponse item in request.OrderDetails)
            {
                Product product = await _context.Products.FindAsync(item.Product.Id);
                if (product == null)
                {
                    return NotFound("Error002");
                }

                order.OrderDetails.Add(new OrderDetail
                {
                    Price = product.Price,
                    Product = product,
                    Quantity = item.Quantity,
                    Remarks = item.Remarks
                });
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return Ok(order);
        }
    }
}
