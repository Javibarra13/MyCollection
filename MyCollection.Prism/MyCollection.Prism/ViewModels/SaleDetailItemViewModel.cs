using MyCollection.Common.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Xaml;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCollection.Prism.ViewModels
{
    public class SaleDetailItemViewModel : SaleDetailResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectSaleDetailCommand;

        public SaleDetailItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectSaleDetailCommand => _selectSaleDetailCommand ?? (_selectSaleDetailCommand = new DelegateCommand(SelectSaleDetail));

        private async void SelectSaleDetail()
        {
            var parameters = new NavigationParameters
            {
                { "saledetail", this }
            };
            await _navigationService.NavigateAsync("SaleDetailPage", parameters);
        }
    }
}
