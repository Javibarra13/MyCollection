using MyCollection.Common.Helpers;
using MyCollection.Common.Models;
using Newtonsoft.Json;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyCollection.Prism.ViewModels
{
    public class SalesPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private CustomerResponse _customer;
        private ObservableCollection<SaleItemViewModel> _sales;

        public SalesPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Customer = JsonConvert.DeserializeObject<CustomerResponse>(Settings.Customer);
            LoadSales();
            Title = "Ventas";
            _navigationService = navigationService;
        }
        public CustomerResponse Customer { get => _customer; set => SetProperty(ref _customer, value); }

        public ObservableCollection<SaleItemViewModel> Sales { get => _sales; set => SetProperty(ref _sales, value); }

        private void LoadSales()
        {
            Sales = new ObservableCollection<SaleItemViewModel>(Customer.Sales.Select(s => new SaleItemViewModel(_navigationService)
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
