using OnSale.Prism.Helpers;
using Prism.Navigation;

namespace OnSale.Prism.ViewModels
{
    public class ShowHistoryPageViewModel : ViewModelBase
    {
        public ShowHistoryPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = Languages.ShowPurchaseHistory;
        }
    }
}
