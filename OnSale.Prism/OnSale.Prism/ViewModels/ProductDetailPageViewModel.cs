using System;
using System.Collections.ObjectModel;
using OnSale.Common.Entities;
using OnSale.Common.Responses;
using OnSale.Prism.Helpers;
using OnSale.Prism.Views;
using Prism.Commands;
using Prism.Navigation;

namespace OnSale.Prism.ViewModels
{
    public class ProductDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private ProductResponse _product;
        private ObservableCollection<ProductImage> _images;
        private DelegateCommand _addToCartCommand;

        public ProductDetailPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            Title = Languages.Details;
        }

        public DelegateCommand AddToCartCommand => _addToCartCommand ?? (_addToCartCommand = new DelegateCommand(AddToCartAsycn));

        public ObservableCollection<ProductImage> Images
        {
            get => _images;
            set => SetProperty(ref _images, value);
        }

        public ProductResponse Product
        {
            get => _product;
            set => SetProperty(ref _product, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("product"))
            {
                Product = parameters.GetValue<ProductResponse>("product");
                Images = new ObservableCollection<ProductImage>(Product.ProductImages);
            }
        }

        private async void AddToCartAsycn()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "product", Product }
            };

            await _navigationService.NavigateAsync(nameof(AddToCartPage), parameters);
        }
    }
}
