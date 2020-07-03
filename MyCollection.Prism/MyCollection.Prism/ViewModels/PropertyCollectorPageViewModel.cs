using MyCollection.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyCollection.Prism.ViewModels
{
    public class PropertyCollectorPageViewModel : ViewModelBase
    {
        private PropertyCollectorResponse _property;
        public PropertyCollectorPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Property";
        }

        public PropertyCollectorResponse PropertyCollector
        {
            get => _property;
            set => SetProperty(ref _property, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("property"))
            {
                PropertyCollector = parameters.GetValue<PropertyCollectorResponse>("property");
                Title = $"Propiedad: {PropertyCollector.Model}";
            }
        }
    }
}
