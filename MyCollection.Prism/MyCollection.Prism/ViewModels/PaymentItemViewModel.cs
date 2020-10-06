using MyCollection.Common.Models;
using Prism.Navigation;

namespace MyCollection.Prism.ViewModels
{
    public class PaymentItemViewModel : PaymentResponse
    {
        private readonly INavigationService _navigationService;

        public PaymentItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
