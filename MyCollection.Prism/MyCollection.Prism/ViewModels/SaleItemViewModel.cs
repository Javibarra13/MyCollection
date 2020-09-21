using MyCollection.Common.Models;
using Prism.Commands;
using Prism.Navigation;
using System;

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
            var parameters = new NavigationParameters
            {
                { "sale", this }
            };
            await _navigationService.NavigateAsync("SalePage", parameters);
        }
    }
}
