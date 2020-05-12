using MyCollection.Common.Models;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MyCollection.Prism.ViewModels
{
    public class PropertyCollectorPageViewModel : ViewModelBase
    {
        private PropertyCollectorResponse _propertyCollector;
        private ObservableCollection<RotatorModel> _imageCollection;
        public PropertyCollectorPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Propiedad";
        }

        public ObservableCollection<RotatorModel> ImageCollection
        {
            get => _imageCollection;
            set => SetProperty(ref _imageCollection, value);
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
                LoadImages();
            }
        }

        private void LoadImages()
    	{
        	var list = new List<RotatorModel>();
        	foreach (var propertyImage in PropertyCollector.PropertyCollectorImages)
        	{
            	list.Add(new RotatorModel { Image = propertyImage.ImageUrl });
        	}

        	ImageCollection = new ObservableCollection<RotatorModel>(list);
    	}
	}

    }

