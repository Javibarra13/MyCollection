using MyCollection.Common.Helpers;
using MyCollection.Common.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

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
