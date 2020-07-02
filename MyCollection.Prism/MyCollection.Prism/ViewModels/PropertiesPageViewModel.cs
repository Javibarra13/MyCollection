using MyCollection.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyCollection.Prism.ViewModels
{
    public class PropertiesPageViewModel : ViewModelBase
    {
        private CollectorResponse _collector;
        private ObservableCollection<PropertyCollectorResponse> _properties;

        public PropertiesPageViewModel(
            INavigationService navigationService) : base(navigationService)
        {
            Title = "Properties";
        }

        public ObservableCollection<PropertyCollectorResponse> Properties
        {
            get => _properties;
            set => SetProperty(ref _properties, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("collector"))
            {
                _collector = parameters.GetValue<CollectorResponse>("collector");
                Title = $"Propiedades de: {_collector.FullName}";
                Properties = new ObservableCollection<PropertyCollectorResponse>(_collector.PropertyCollectors);
            }
        }
    }
}
