using MyCollection.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyCollection.Prism.ViewModels
{
    public class EditPaymentPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private SaleResponse _sale;
        private bool _isRunning;
        private bool _isEnabled;
        private bool _isEdit;

        public EditPaymentPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Agregar Abono";
            _isEnabled = true;
            _navigationService = navigationService;
        }

        public bool IsRunning
        { 
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEdit
        {
            get => _isEdit;
            set => SetProperty(ref _isEdit, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        public SaleResponse Sale
        {
            get => _sale;
            set => SetProperty(ref _sale, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("sale"))
            {
                Sale = parameters.GetValue<SaleResponse>("sale");
                IsEdit = true;
                Title = "Editar Abono";
            }
            else
            {
                Title = "Agregar Abono";
                Sale = new SaleResponse { Collector = _sale.Collector};
                IsEdit = false;
            }
        }
    }
}
