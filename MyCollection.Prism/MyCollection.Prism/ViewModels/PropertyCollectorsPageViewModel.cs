using MyCollection.Common.Models;
using MyCollection.Prism.Views;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyCollection.Prism.ViewModels
{
    public class PropertyCollectorsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private CollectorResponse _collector;
        private ObservableCollection<PropertyCollectorItemViewModel> _propertyCollectors;

        public PropertyCollectorsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Properties";
            _navigationService = navigationService;
        }

        public ObservableCollection<PropertyCollectorItemViewModel> PropertyCollectors
        {
            get => _propertyCollectors;
            set => SetProperty(ref _propertyCollectors, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("collector"))
            {
                _collector = parameters.GetValue<CollectorResponse>("collector");
                Title = $"Properties of: {_collector.FullName}";
                PropertyCollectors = new ObservableCollection<PropertyCollectorItemViewModel>(_collector.PropertyCollectors.Select(pc => new PropertyCollectorItemViewModel(_navigationService)
                {
                    Serie = pc.Serie,
                    Company = pc.Company,
                    Model = pc.Model,
                    Colour = pc.Colour,
                    Price = pc.Price,
                    IsAvailable = pc.IsAvailable,
                    Remarks = pc.Remarks,
                    Id = pc.Id,
                    PropertyType = pc.PropertyType,
                    PropertyCollectorImages = pc.PropertyCollectorImages
                }).ToList());
            }
        }
    }
}
