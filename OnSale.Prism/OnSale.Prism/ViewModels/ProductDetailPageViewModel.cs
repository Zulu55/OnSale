using OnSale.Common.Entities;
using Prism.Navigation;

namespace OnSale.Prism.ViewModels
{
    public class ProductDetailPageViewModel : ViewModelBase
    {
        private Product _product;

        public ProductDetailPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Product";
        }

        public Product Product
        {
            get => _product;
            set => SetProperty(ref _product, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("product"))
            {
                Product = parameters.GetValue<Product>("product");
                Title = Product.Name;
            }
        }
    }
}
