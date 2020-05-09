using MyCollection.Common.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyCollection.Prism.ViewModels
{
    public class PropertyCollectorsPageViewModel : ViewModelBase
    {
        private CollectorResponse _collector;
        private ObservableCollection<PropertyCollectorResponse> _propertyCollectors;

        public PropertyCollectorsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Properties";
        }

        public ObservableCollection<PropertyCollectorResponse> PropertyCollectors
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
                PropertyCollectors = new ObservableCollection<PropertyCollectorResponse>(_collector.PropertyCollectors);
            }
        }
    }
}
