using OnSale.Common.Entities;
using OnSale.Web.Models;
using System;
using System.Threading.Tasks;

namespace OnSale.Web.Helpers
{
    public interface IConverterHelper
    {
        Category ToCategory(CategoryViewModel model, Guid imageId, bool isNew);

        CategoryViewModel ToCategoryViewModel(Category category);

        Task<Products> ToProductAsync(ProductViewModel model, bool isNew);

        ProductViewModel ToProductViewModel(Products product);
    }
}
