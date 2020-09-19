using MyCollection.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyCollection.Prism.ViewModels
{
    public class SalesPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private CollectorResponse _collector;

        public SalesPageViewModel(
            INavigationService navigationService) : base(navigationService)
        {
            Title = "Ventas";
            _navigationService = navigationService;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("collector"))
            {
                _collector = parameters.GetValue<CollectorResponse>("collector");
                Title = $"Ventas de: {_collector.FullName}";
            }
        }
    }
}
