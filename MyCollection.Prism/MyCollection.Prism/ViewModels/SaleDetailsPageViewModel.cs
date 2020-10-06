using MyCollection.Common.Helpers;
using MyCollection.Common.Models;
using Newtonsoft.Json;
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
            Title = "Detalles";
            LoadSale();
        }

        public SaleResponse Sale { get => _sale; set => SetProperty(ref _sale, value); }

        public ObservableCollection<SaleDetailItemViewModel> SaleDetails { get => _saleDetails; set => SetProperty(ref _saleDetails, value); }

        private void LoadSale()
        {
            _sale = JsonConvert.DeserializeObject<SaleResponse>(Settings.Sale);
            Title = $"Detalles Venta";
            SaleDetails = new ObservableCollection<SaleDetailItemViewModel>(_sale.SaleDetails.Select(sd => new SaleDetailItemViewModel(_navigationService)
            {
                Id = sd.Id,
                Name = sd.Name,
                Price = sd.Price,
                Quantity = sd.Quantity,
                Sale = sd.Sale,
                Product = sd.Product
            }).ToList());
        }
    }
}
