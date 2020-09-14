using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OnSale.Common.Helpers;
using OnSale.Common.Requests;
using OnSale.Common.Responses;
using OnSale.Common.Services;
using OnSale.Prism.Helpers;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Essentials;

namespace OnSale.Prism.ViewModels
{
    public class ChangePasswordPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private bool _isRunning;
        private bool _isEnabled;
        private DelegateCommand _changePasswordCommand;

        public ChangePasswordPageViewModel(INavigationService navigationService, IApiService apiService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            IsEnabled = true;
            Title = Languages.ChangePassword;
        }
        public DelegateCommand ChangePasswordCommand => _changePasswordCommand ??
            (_changePasswordCommand = new DelegateCommand(ChangePasswordAsync));

        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }

        public string PasswordConfirm { get; set; }

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

        private async void ChangePasswordAsync()
        {
            var isValid = await ValidateDataAsync();
            if (!isValid)
            {
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }

            ChangePasswordRequest request = new ChangePasswordRequest
            {
                NewPassword = NewPassword,
                OldPassword = CurrentPassword,
            };

            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.ChangePasswordAsync(url, "/api", "/Account/ChangePassword", request, token.Token);

            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                if (response.Message == "Error001")
                {
                    await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.Error001, Languages.Accept);
                }
                else if (response.Message == "Error005")
                {
                    await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.Error005, Languages.Accept);
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                }

                return;
            }

            await App.Current.MainPage.DisplayAlert(Languages.Ok, Languages.ChangePassworrdMessage, Languages.Accept);
            await _navigationService.GoBackAsync();
        }

        private async Task<bool> ValidateDataAsync()
        {
            if (string.IsNullOrEmpty(CurrentPassword))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.CurrentPasswordError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(NewPassword) || NewPassword?.Length < 6)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.NewPasswordError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(PasswordConfirm))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConfirmNewPasswordError1, Languages.Accept);
                return false;
            }

            if (NewPassword != PasswordConfirm)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConfirmNewPasswordError2, Languages.Accept);
                return false;
            }

            return true;
        }
    }
}
