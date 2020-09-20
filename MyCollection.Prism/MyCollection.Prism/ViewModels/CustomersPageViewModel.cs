using MyCollection.Common.Models;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyCollection.Prism.ViewModels
{
    public class CustomersPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private CollectorResponse _collector;
        private ObservableCollection<CustomerItemViewModel> _customers;

        public CustomersPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Clientes";
            _navigationService = navigationService;
        }

        public ObservableCollection<CustomerItemViewModel> Customers
        {
            get => _customers;
            set => SetProperty(ref _customers, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("collector"))
            {
                _collector = parameters.GetValue<CollectorResponse>("collector");
                Title = $"Clientes de: {_collector.FullName}";
                Customers = new ObservableCollection<CustomerItemViewModel>(_collector.Customers.Select(c => new CustomerItemViewModel(_navigationService)
                { 
                    Id = c.Id,
                    Name = c.Name,
                    Address = c.Address,
                    Neighborhood = c.Neighborhood,
                    City = c.City,
                    PhoneNumber = c.PhoneNumber,
                    PostalCode = c.PostalCode,
                    Remarks = c.Remarks,
                    RefName = c.RefName,
                    RefAddress = c.RefAddress,
                    RefPhone = c.RefPhone,
                    RefName2 = c.RefName2,
                    RefAddress2 = c.RefAddress2,
                    RefPhone2 = c.RefPhone2,
                    House = c.House,
                    Collector = c.Collector,
                    CustomerImages = c.CustomerImages,
                    Sales = c.Sales,
                    Payments = c.Payments
                }).ToList());
            }
        }
    }
}
