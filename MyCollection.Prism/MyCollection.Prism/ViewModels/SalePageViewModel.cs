using MyCollection.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyCollection.Prism.ViewModels
{
    public class SalePageViewModel : ViewModelBase
    {
        private SaleResponse _sale;
        private readonly INavigationService _navigationService;

        public SalePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Venta";
            _navigationService = navigationService;
        }

        public SaleResponse Sale { get => _sale; set => SetProperty(ref _sale, value); }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.ContainsKey("sale"))
            {
                Sale = parameters.GetValue<SaleResponse>("sale");
            }
        }
    }
}
