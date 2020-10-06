using MyCollection.Common.Models;
using Prism.Navigation;

namespace MyCollection.Prism.ViewModels
{
    public class SaleDetailItemViewModel : SaleDetailResponse
    {
        private readonly INavigationService _navigationService;

        public SaleDetailItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
