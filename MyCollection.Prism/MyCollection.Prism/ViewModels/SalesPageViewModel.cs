﻿using MyCollection.Common.Models;
using Prism.Navigation;
using System.Collections.ObjectModel;

namespace MyCollection.Prism.ViewModels
{
    public class SalesPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private CollectorResponse _collector;
        private ObservableCollection<SaleResponse> _sales;

        public SalesPageViewModel(
            INavigationService navigationService) : base(navigationService)
        {
            Title = "Ventas";
            _navigationService = navigationService;
        }

        public ObservableCollection<SaleResponse> Sales
        {
            get => _sales;
            set => SetProperty(ref _sales, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("collector"))
            {
                _collector = parameters.GetValue<CollectorResponse>("collector");
                Title = $"Ventas de: {_collector.FullName}";
                Sales = new ObservableCollection<SaleResponse>(_collector.Sales);
            }
        }
    }
}
