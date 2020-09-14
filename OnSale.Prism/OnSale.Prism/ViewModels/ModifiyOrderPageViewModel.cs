using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OnSale.Common.Entities;
using OnSale.Common.Helpers;
using OnSale.Common.Models;
using OnSale.Prism.Helpers;
using OnSale.Prism.ItemViewModels;
using Prism.Commands;
using Prism.Navigation;

namespace OnSale.Prism.ViewModels
{
    public class ModifiyOrderPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private ProductItemViewModel _product;
        private ObservableCollection<ProductImage> _images;
        private bool _isRunning;
        private bool _isEnabled;
        private float _quantity;
        private string _remarks;
        private DelegateCommand _saveCommand;
        private DelegateCommand _deleteCommand;

        public ModifiyOrderPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            Title = Languages.ModifyOrder;
            IsEnabled = true;
        }

        public DelegateCommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(SaveAsync));

        public DelegateCommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new DelegateCommand(DeleteAsync));

        public float Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value);
        }

        public string Remarks
        {
            get => _remarks;
            set => SetProperty(ref _remarks, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        public ObservableCollection<ProductImage> Images
        {
            get => _images;
            set => SetProperty(ref _images, value);
        }

        public ProductItemViewModel Product
        {
            get => _product;
            set => SetProperty(ref _product, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("product"))
            {
                Product = parameters.GetValue<ProductItemViewModel>("product");
                Images = new ObservableCollection<ProductImage>(Product.ProductImages);
                Quantity = Product.Quantity;
                Remarks = Product.Remarks;
            }
        }

        private async void SaveAsync()
        {
            bool isValid = await ValidateDataAsync();
            if (!isValid)
            {
                return;
            }

            List<OrderDetail> orderDetails = JsonConvert.DeserializeObject<List<OrderDetail>>(Settings.OrderDetails);
            if (orderDetails == null)
            {
                return;
            }

            foreach (var orderDetail in orderDetails)
            {
                if (orderDetail.Product.Id == Product.Id)
                {
                    orderDetail.Quantity = Quantity;
                    orderDetail.Remarks = Remarks;
                    break;
                }
            }

            Settings.OrderDetails = JsonConvert.SerializeObject(orderDetails);
            await _navigationService.GoBackAsync();
        }

        private async Task<bool> ValidateDataAsync()
        {
            if (Quantity == 0)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.QuantityError, Languages.Accept);
                return false;
            }

            return true;
        }

        private async void DeleteAsync()
        {
            bool answer = await App.Current.MainPage.DisplayAlert(Languages.Delete, Languages.DeleteProductInOrderConfirm, Languages.Yes, Languages.No);
            if (!answer)
            {
                return;
            }

            List<OrderDetail> orderDetails = JsonConvert.DeserializeObject<List<OrderDetail>>(Settings.OrderDetails);
            if (orderDetails == null)
            {
                return;
            }

            foreach (var orderDetail in orderDetails)
            {
                if (orderDetail.Product.Id == Product.Id)
                {
                    orderDetails.Remove(orderDetail);
                    break;
                }
            }

            Settings.OrderDetails = JsonConvert.SerializeObject(orderDetails);
            await _navigationService.GoBackAsync();
        }
    }
}
