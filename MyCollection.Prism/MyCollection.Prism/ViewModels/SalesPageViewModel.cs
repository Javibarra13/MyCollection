using MyCollection.Common.Models;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyCollection.Prism.ViewModels
{
    public class SalesPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private CollectorResponse _collector;
        private CustomerResponse _customer;
        private ObservableCollection<SaleItemViewModel> _sales;

        public SalesPageViewModel(
            INavigationService navigationService) : base(navigationService)
        {
            Title = "Ventas";
            _navigationService = navigationService;
        }

        public ObservableCollection<SaleItemViewModel> Sales{ get => _sales; set => SetProperty(ref _sales, value); }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("customer"))
            {
                _customer = parameters.GetValue<CustomerResponse>("customer");
                LoadSales();
            }
        }

        private void LoadSales()
        {
            Sales = new ObservableCollection<SaleItemViewModel>(_customer.Sales.Select(s => new SaleItemViewModel(_navigationService)
            { 
                Id = s.Id,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                Payment = s.Payment,
                Deposit = s.Deposit,
                Remarks = s.Remarks,
                TypePayment = s.TypePayment,
                DayPayment = s.DayPayment,
                Seller = s.Seller,
                Collector = s.Collector,
                Customer = s.Customer,
                Payments = s.Payments,
                SaleDetails = s.SaleDetails
            }).ToList());
        }
    }
}
