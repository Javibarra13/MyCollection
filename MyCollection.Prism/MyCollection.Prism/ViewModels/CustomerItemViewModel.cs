using MyCollection.Common.Helpers;
using MyCollection.Common.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;

namespace MyCollection.Prism.ViewModels
{
    public class CustomerItemViewModel : CustomerResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectCustomerCommand;
        public CustomerItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectCustomerCommand => _selectCustomerCommand ?? (_selectCustomerCommand = new DelegateCommand(SelectCustomer));

        private async void SelectCustomer()
        {
            Settings.Customer = JsonConvert.SerializeObject(this);
            await _navigationService.NavigateAsync("DetailsTabbedPage");
        }
    }
}
