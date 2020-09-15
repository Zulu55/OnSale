using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;
using OnSale.Common.Helpers;
using OnSale.Common.Responses;
using OnSale.Common.Services;
using OnSale.Prism.Helpers;
using OnSale.Prism.ItemViewModels;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Essentials;

namespace OnSale.Prism.ViewModels
{
    public class ShowHistoryPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private ObservableCollection<OrderItemViewModel> _orders;
        private bool _isRunning;
        private string _search;
        private int _cartNumber;
        private List<OrderResponse> _myOrders;
        private DelegateCommand _searchCommand;

        public ShowHistoryPageViewModel(INavigationService navigationService, IApiService apiService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = Languages.ShowPurchaseHistory;
            LoadOrdersAsync();
        }

        public DelegateCommand SearchCommand => _searchCommand ?? (_searchCommand = new DelegateCommand(ShowOrders));
        public string Search
        {
            get => _search;
            set
            {
                SetProperty(ref _search, value);
                ShowOrders();
            }
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public ObservableCollection<OrderItemViewModel> Orders
        {
            get => _orders;
            set => SetProperty(ref _orders, value);
        }

        private async void LoadOrdersAsync()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }

            IsRunning = true;
            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetListAsync<OrderResponse>(url, "/api", "/Orders", token.Token);
            IsRunning = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            _myOrders = (List<OrderResponse>)response.Result;
            ShowOrders();
        }

        private void ShowOrders()
        {
            if (_myOrders == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(Search))
            {
                Orders = new ObservableCollection<OrderItemViewModel>(_myOrders.Select(o => new OrderItemViewModel(_navigationService)
                {
                    Date = o.Date,
                    DateConfirmed = o.DateConfirmed,
                    DateSent = o.DateSent,
                    Id = o.Id,
                    OrderDetails = o.OrderDetails,
                    OrderStatus = o.OrderStatus,
                    PaymentMethod = o.PaymentMethod,
                    Remarks = o.Remarks,
                    User = o.User
                })
                    .OrderByDescending(o => o.Date)
                    .ToList());
            }
            else
            {
                Orders = new ObservableCollection<OrderItemViewModel>(_myOrders.Select(o => new OrderItemViewModel(_navigationService)
                {
                    Date = o.Date,
                    DateConfirmed = o.DateConfirmed,
                    DateSent = o.DateSent,
                    Id = o.Id,
                    OrderDetails = o.OrderDetails,
                    OrderStatus = o.OrderStatus,
                    PaymentMethod = o.PaymentMethod,
                    Remarks = o.Remarks,
                    User = o.User
                })
                    .Where(o => o.Value.ToString().Contains(Search))
                    .OrderByDescending(o => o.Date)
                    .ToList());
            }
        }
    }
}
