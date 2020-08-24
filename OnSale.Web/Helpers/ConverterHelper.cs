using Microsoft.AspNetCore.Rewrite.Internal;
using OnSale.Common.Entities;
using OnSale.Web.Data;
using OnSale.Web.Models;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace OnSale.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(DataContext context, ICombosHelper combosHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
        }

        public Category ToCategory(CategoryViewModel model, Guid imageId, bool isNew)
        {
            return new Category
            {
                Id = isNew ? 0 : model.Id,
                ImageId = imageId,
                Name = model.Name
            };
        }

        public CategoryViewModel ToCategoryViewModel(Category category)
        {
            return new CategoryViewModel
            {
                Id = category.Id,
                ImageId = category.ImageId,
                Name = category.Name
            };
        }

        public async Task<Product> ToProductAsync(ProductViewModel model, bool isNew)
        {
            return new Product
            {
                Category = await _context.Categories.FindAsync(model.CategoryId),
                Description = model.Description,
                Id = isNew ? 0 : model.Id,
                IsActive = model.IsActive,
                IsStarred = model.IsStarred,
                Name = model.Name,
                Price = ToPrice(model.PriceString),
                ProductImages = model.ProductImages
            };
        }

        private decimal ToPrice(string priceString)
        {
            string nds = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            if (nds == ".")
            {
                priceString = priceString.Replace(',', '.');
            }
            else
            {
                priceString = priceString.Replace('.', ',');
            }

            return decimal.Parse(priceString);
        }

        public ProductViewModel ToProductViewModel(Product product)
        {
            return new ProductViewModel
            {
                Categories = _combosHelper.GetComboCategories(),
                Category = product.Category,
                CategoryId = product.Category.Id,
                Description = product.Description,
                Id = product.Id,
                IsActive = product.IsActive,
                IsStarred = product.IsStarred,
                Name = product.Name,
                Price = product.Price,
                PriceString = $"{product.Price}",
                ProductImages = product.ProductImages
            };
        }
    }
}
