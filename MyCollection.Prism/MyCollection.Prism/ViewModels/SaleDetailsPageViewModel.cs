using MyCollection.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyCollection.Prism.ViewModels
{
    public class SaleDetailsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private SaleResponse _sale;
        private ObservableCollection<SaleDetailItemViewModel> _saleDetails;
        public SaleDetailsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            Title = "Detalles de Compra";
        }

        public ObservableCollection<SaleDetailItemViewModel> SaleDetails
        {
            get => _saleDetails;
            set => SetProperty(ref _saleDetails, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.ContainsKey("sale"))
            {
                _sale = parameters.GetValue<SaleResponse>("sale");
                LoadSaleDetails();
            }
        }

        private void LoadSaleDetails()
        {
            SaleDetails = new ObservableCollection<SaleDetailItemViewModel>(_sale.SaleDetails.Select(sd => new SaleDetailItemViewModel(_navigationService)
            { 
                Id = sd.Id,
                Name = sd.Name,
                Price = sd.Price,
                Quantity = sd.Quantity
            }).ToList());
        }
    }
}
