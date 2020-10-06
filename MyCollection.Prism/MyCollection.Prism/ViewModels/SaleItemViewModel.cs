using MyCollection.Common.Helpers;
using MyCollection.Common.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;

namespace MyCollection.Prism.ViewModels
{
    public class SaleItemViewModel : SaleResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectSaleCommand;

        public SaleItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectSaleCommand => _selectSaleCommand ?? (_selectSaleCommand = new DelegateCommand(SelectSale));

        private async void SelectSale()
        {
            Settings.Sale = JsonConvert.SerializeObject(this);
            await _navigationService.NavigateAsync("SaleDetailsTabbedPage");
        }
    }
}
