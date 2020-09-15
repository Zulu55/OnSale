using System.Collections.ObjectModel;
using Newtonsoft.Json;
using OnSale.Common.Enums;
using OnSale.Common.Helpers;
using OnSale.Common.Responses;
using OnSale.Common.Services;
using OnSale.Prism.Helpers;
using OnSale.Prism.Views;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Essentials;

namespace OnSale.Prism.ViewModels
{
    public class OrderPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private OrderResponse _order;
        private ObservableCollection<OrderDetailResponse> _orderDetails;
        private bool _isVisible;
        private DelegateCommand _updateRemarksCommand;
        private DelegateCommand _cancelOrderCommand;

        public OrderPageViewModel(INavigationService navigationService, IApiService apiService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = Languages.Order;
        }

        public DelegateCommand UpdateRemarksCommand => _updateRemarksCommand ??
            (_updateRemarksCommand = new DelegateCommand(UpdateRemarksAsync));

        public DelegateCommand CancelOrderCommand => _cancelOrderCommand ??
            (_cancelOrderCommand = new DelegateCommand(CancelOrderAsync));

        public OrderResponse Order
        {
            get => _order;
            set => SetProperty(ref _order, value);
        }

        public bool IsVisible
        {
            get => _isVisible;
            set => SetProperty(ref _isVisible, value);
        }

        public ObservableCollection<OrderDetailResponse> OrderDetails
        {
            get => _orderDetails;
            set => SetProperty(ref _orderDetails, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.ContainsKey("order"))
            {
                Order = parameters.GetValue<OrderResponse>("order");
                OrderDetails = new ObservableCollection<OrderDetailResponse>(Order.OrderDetails);
                if (Order.OrderStatus == OrderStatus.Pending)
                {
                    IsVisible = true;
                }
            }
        }

        private async void UpdateRemarksAsync()
        {
            if (string.IsNullOrEmpty(Order.Remarks))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.RemarksError, Languages.Accept);
                return;
            }

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }

            string url = App.Current.Resources["UrlAPI"].ToString();
            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            Response response = await _apiService.PutAsync(url, "api", "/Orders", Order, token.Token);

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            await App.Current.MainPage.DisplayAlert(Languages.Ok, Languages.OrderUpdatedOk, Languages.Accept);
            await _navigationService.NavigateAsync($"/{nameof(OnSaleMasterDetailPage)}/NavigationPage/{nameof(ProductsPage)}");
        }

        private async void CancelOrderAsync()
        {
            bool asnwer = await App.Current.MainPage.DisplayAlert(Languages.Question, Languages.CancelOrdenConfirm, Languages.Yes, Languages.No);
            if (!asnwer)
            {
                return;
            }

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }

            Order.OrderStatus = OrderStatus.Cancelled;
            string url = App.Current.Resources["UrlAPI"].ToString();
            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            Response response = await _apiService.PutAsync(url, "api", "/Orders", Order, token.Token);

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            await App.Current.MainPage.DisplayAlert(Languages.Ok, Languages.OrderUpdatedOk, Languages.Accept);
            await _navigationService.NavigateAsync($"/{nameof(OnSaleMasterDetailPage)}/NavigationPage/{nameof(ProductsPage)}");
        }
    }
}
