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
        private readonly INavigationService _navigationService;
        private CollectorResponse _collector;
        private ObservableCollection<PropertyCollectorItemViewModel> _properties;

        public PropertiesPageViewModel(
            INavigationService navigationService) : base(navigationService)
        {
            Title = "Properties";
            _navigationService = navigationService;
        }

        public ObservableCollection<PropertyCollectorItemViewModel> Properties
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
                Properties = new ObservableCollection<PropertyCollectorItemViewModel>(_collector.PropertyCollectors.Select(p => new PropertyCollectorItemViewModel(_navigationService)
                { 
                    Id = p.Id,
                    Serie = p.Serie,
                    Company = p.Company,
                    Model = p.Model,
                    Colour = p.Colour,
                    Price = p.Price,
                    IsAvailable = p.IsAvailable,
                    Remarks = p.Remarks,
                    PropertyType = p.PropertyType,
                    PropertyCollectorImages = p.PropertyCollectorImages,
                }).ToList());
            }
        }
    }
}
