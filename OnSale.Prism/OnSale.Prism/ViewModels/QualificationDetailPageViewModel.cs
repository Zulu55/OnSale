using OnSale.Common.Responses;
using OnSale.Prism.Helpers;
using Prism.Navigation;

namespace OnSale.Prism.ViewModels
{
    public class QualificationDetailPageViewModel : ViewModelBase
    {
        private QualificationResponse _qualification;

        public QualificationDetailPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = Languages.Qualification;
        }

        public QualificationResponse Qualification
        {
            get => _qualification;
            set => SetProperty(ref _qualification, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("qualification"))
            {
                Qualification = parameters.GetValue<QualificationResponse>("qualification");
            }
        }
    }
}