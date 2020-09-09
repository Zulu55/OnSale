using OnSale.Common.Responses;
using OnSale.Prism.Helpers;
using Prism.Navigation;

namespace OnSale.Prism.ViewModels
{
    public class ProductTabbedPageViewModel : ViewModelBase
    {
        public ProductTabbedPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = Languages.Product;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("product"))
            {
                ProductResponse product = parameters.GetValue<ProductResponse>("product");
                Title = product.Name;
            }
        }
    }
}
