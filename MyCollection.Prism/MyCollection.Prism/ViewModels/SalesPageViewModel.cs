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
    public class SalesPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private CollectorResponse _collector;
        private ObservableCollection<SaleItemViewModel> _sales;

        public SalesPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Abonos";
            _navigationService = navigationService;
        }

        public ObservableCollection<SaleItemViewModel> Sales
        {
            get => _sales;
            set => SetProperty(ref _sales, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if(parameters.ContainsKey("collector"))
            {
                _collector = parameters.GetValue<CollectorResponse>("collector");
                Title = $"Clientes de: {_collector.FullName}";
                Sales = new ObservableCollection<SaleItemViewModel>(_collector.Sales.Select(s => new SaleItemViewModel(_navigationService)
                { 
                    Id = s.Id,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    Payment = s.Payment,
                    Deposit = s.Deposit,
                    Remarks = s.Remarks,
                    Collector = s.Collector,
                    DayPayment = s.DayPayment,
                    Seller = s.Seller,
                    Customer = s.Customer,
                    House = s.House,
                    TypePayment = s.TypePayment,
                    Warehouse = s.Warehouse,
                    State = s.State,
                    SaleDetails = s.SaleDetails
                }).ToList());
            }
        }
    }
}
