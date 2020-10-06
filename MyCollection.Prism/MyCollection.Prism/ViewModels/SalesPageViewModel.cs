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
            _navigationService = navigationService;
            Title = "Ventas";
            LoadCustomer();
        }

        public ObservableCollection<SaleItemViewModel> Sales { get => _sales; set => SetProperty(ref _sales, value); }

        private void LoadCustomer()
        {
            _customer = JsonConvert.DeserializeObject<CustomerResponse>(Settings.Customer);
            Title = $"Ventas";
            Sales = new ObservableCollection<SaleItemViewModel>(_customer.Sales.Select(s => new SaleItemViewModel(_navigationService)
            {
                Id = s.Id,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                Payment = s.Payment,
                Deposit = s.Deposit,
                Remarks = s.Remarks,
                Pending = s.Pending,
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
