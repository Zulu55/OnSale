using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnSale.Common.Entities;
using OnSale.Common.Enums;
using OnSale.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnSale.Web.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;

        public CombosHelper(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetComboCategories()
        {
            List<SelectListItem> list = _context.Categories.Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = $"{t.Id}"
            })
                .OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a category...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboCities(int departmentId)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            Department department = _context.Departments
                .Include(d => d.Cities)
                .FirstOrDefault(d => d.Id == departmentId);
            if (department != null)
            {
                list = department.Cities.Select(t => new SelectListItem
                {
                    Text = t.Name,
                    Value = $"{t.Id}"
                })
                    .OrderBy(t => t.Text)
                    .ToList();
            }

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a city...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboCountries()
        {
            List<SelectListItem> list = _context.Countries.Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = $"{t.Id}"
            })
                .OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a country...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboDepartments(int countryId)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            Country country = _context.Countries
                .Include(c => c.Departments)
                .FirstOrDefault(c => c.Id == countryId);
            if (country != null)
            {
                list = country.Departments.Select(t => new SelectListItem
                {
                    Text = t.Name,
                    Value = $"{t.Id}"
                })
                    .OrderBy(t => t.Text)
                    .ToList();
            }

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a department...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetOrderStatuses()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = OrderStatus.Pending.ToString() },
                new SelectListItem { Value = "1", Text = OrderStatus.Spreading.ToString() },
                new SelectListItem { Value = "2", Text = OrderStatus.Sent.ToString() },
                new SelectListItem { Value = "3", Text = OrderStatus.Confirmed.ToString() },
                new SelectListItem { Value = "4", Text = OrderStatus.Cancelled.ToString() }
            };
        }
    }
}
