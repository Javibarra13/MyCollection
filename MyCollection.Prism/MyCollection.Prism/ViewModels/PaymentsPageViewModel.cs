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
    public class PaymentsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private SaleResponse _sale;
        private ObservableCollection<PaymentItemViewModel> _payments;
        private DelegateCommand _addPaymentCommand; 

        public PaymentsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            Title = "Abonos";
            LoadSale();
        }

        public DelegateCommand AddPaymentCommand => _addPaymentCommand ?? (_addPaymentCommand = new DelegateCommand(AddPaymentAsync));

        public SaleResponse Sale { get => _sale; set => SetProperty(ref _sale, value); }

        public ObservableCollection<PaymentItemViewModel> Payments { get => _payments; set => SetProperty(ref _payments, value); }

        private void LoadSale()
        {
            _sale = JsonConvert.DeserializeObject<SaleResponse>(Settings.Sale);
            Title = $"Abonos";
            Payments = new ObservableCollection<PaymentItemViewModel>(_sale.Payments.Select(p => new PaymentItemViewModel(_navigationService)
            {
                Id = p.Id,
                Collector = p.Collector,
                Concept = p.Concept,
                Customer = p.Customer,
                Date = p.Date,
                Deposit = p.Deposit,
                Sale = p.Sale,
                Type = p.Type,
                Latitude = p.Sale,
                Longitude = p.Longitude
            }).ToList());
        }

        private async void AddPaymentAsync()
        {
            await _navigationService.NavigateAsync("AddPaymentPage");
        }
    }
}
