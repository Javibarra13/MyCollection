using MyCollection.Common.Models;
using Prism.Navigation;
using System.Collections.ObjectModel;

namespace MyCollection.Prism.ViewModels
{
    public class CustomersPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private CollectorResponse _collector;
        private ObservableCollection<CustomerResponse> _customers;

        public CustomersPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Clientes";
            _navigationService = navigationService;
        }

        public ObservableCollection<CustomerResponse> Customers
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
                Customers = new ObservableCollection<CustomerResponse>(_collector.Customers);
            }
        }
    }
}
