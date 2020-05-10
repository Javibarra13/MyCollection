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
        private PropertyCollectorResponse _propertyCollector;
        public PropertyCollectorPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Propiedad";
        }

        public PropertyCollectorResponse PropertyCollector
        {
            get => _propertyCollector;
            set => SetProperty(ref _propertyCollector, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.ContainsKey("propertyCollector"))
            {
                PropertyCollector = parameters.GetValue<PropertyCollectorResponse>("propertyCollector");
                Title = $"Property: {PropertyCollector.Model}";
            }
        }
    }
}
