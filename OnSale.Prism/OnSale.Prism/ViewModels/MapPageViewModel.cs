using OnSale.Prism.Helpers;
using Prism.Navigation;

namespace OnSale.Prism.ViewModels
{
    public class MapPageViewModel : ViewModelBase
    {
        public MapPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = Languages.Buyers;
        }
    }
}
