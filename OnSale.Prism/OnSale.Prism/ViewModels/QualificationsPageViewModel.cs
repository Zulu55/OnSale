using System;
using System.Collections.ObjectModel;
using System.Linq;
using OnSale.Common.Helpers;
using OnSale.Common.Responses;
using OnSale.Prism.Helpers;
using OnSale.Prism.ItemViewModels;
using OnSale.Prism.Views;
using Prism.Commands;
using Prism.Navigation;

namespace OnSale.Prism.ViewModels
{
    public class QualificationsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private ProductResponse _product;
        private bool _isRunning;
        private ObservableCollection<QualificationItemViewModel> _qualifications;
        private DelegateCommand _addQualificationCommand;

        public QualificationsPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            Title = Languages.Qualifications;
        }

        public DelegateCommand AddQualificationCommand => _addQualificationCommand ?? (_addQualificationCommand = new DelegateCommand(AddQualificationAsync));

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public ObservableCollection<QualificationItemViewModel> Qualifications
        {
            get => _qualifications;
            set => SetProperty(ref _qualifications, value);
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);

            if (parameters.ContainsKey("product"))
            {
                LoadProduct(parameters);
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("product"))
            {
                LoadProduct(parameters);
            }
        }

        private void LoadProduct(INavigationParameters parameters)
        {
            IsRunning = true;
            _product = parameters.GetValue<ProductResponse>("product");
            if (_product.Qualifications != null)
            {
                Qualifications = new ObservableCollection<QualificationItemViewModel>(
                    _product.Qualifications.Select(q => new QualificationItemViewModel(_navigationService)
                    {
                        Date = q.Date,
                        Id = q.Id,
                        Remarks = q.Remarks,
                        Score = q.Score
                    })
                    .OrderByDescending(q => q.Date)
                    .ToList());
            }

            IsRunning = false;
        }


        private async void AddQualificationAsync()
        {
            if (Settings.IsLogin)
            {
                await _navigationService.NavigateAsync(nameof(AddQualificationPage));
            }
            else
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.LoginFirstMessage, Languages.Accept);
                NavigationParameters parameters = new NavigationParameters
                {
                    { "pageReturn", nameof(AddQualificationPage) }
                };

                await _navigationService.NavigateAsync($"/{nameof(OnSaleMasterDetailPage)}/NavigationPage/{nameof(LoginPage)}", parameters);
            }
        }
    }
}
