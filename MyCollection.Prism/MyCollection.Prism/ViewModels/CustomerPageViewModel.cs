using MyCollection.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyCollection.Prism.ViewModels
{
    public class CustomerPageViewModel : ViewModelBase
    {
        private CustomerResponse _customer;
        private readonly INavigationService _navigationService;

        public CustomerPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Cliente";
            _navigationService = navigationService;
        }

        public CustomerResponse Customer { get => _customer; set => SetProperty(ref _customer, value); }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("customer"))
            {
                Customer = parameters.GetValue<CustomerResponse>("customer");
                Title = $"Cliente: {Customer.Name}";
            }
        }
    }
}
