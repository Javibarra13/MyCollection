﻿using MyCollection.Common.Helpers;
using MyCollection.Common.Models;
using Newtonsoft.Json;
using Prism.Navigation;

namespace MyCollection.Prism.ViewModels
{
    public class DetailsTabbedPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public DetailsTabbedPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            var customer = JsonConvert.DeserializeObject<CustomerResponse>(Settings.Customer);
            Title = $"Cliente: {customer.Name}";
            _navigationService = navigationService;
        }
    }
}
