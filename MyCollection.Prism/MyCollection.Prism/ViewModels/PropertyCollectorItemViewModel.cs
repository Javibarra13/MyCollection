using MyCollection.Common.Helpers;
using MyCollection.Common.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Prism.Xaml;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCollection.Prism.ViewModels
{
    public class PropertyCollectorItemViewModel : PropertyCollectorResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectPropertyCollectorCommand;

        public PropertyCollectorItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectPropertyCollectorCommand => _selectPropertyCollectorCommand ?? (_selectPropertyCollectorCommand = new DelegateCommand(SelectPropertyCollector));
        
        private async void SelectPropertyCollector()
        {
            var parameters = new NavigationParameters
            {
                { "property", this }
            };
            await _navigationService.NavigateAsync("PropertyCollectorPage", parameters);
        }
    }
}
