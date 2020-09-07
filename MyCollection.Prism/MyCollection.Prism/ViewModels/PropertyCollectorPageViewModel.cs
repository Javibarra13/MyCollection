﻿using MyCollection.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyCollection.Prism.ViewModels
{
    public class PropertyCollectorPageViewModel : ViewModelBase
    {
        private PropertyCollectorResponse _property;
        private ObservableCollection<RotatorModel> _imageCollection;

        public PropertyCollectorPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Property";
        }

        public ObservableCollection<RotatorModel> ImageCollection
        {
            get => _imageCollection;
            set => SetProperty(ref _imageCollection, value);
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
                LoadImages();
            }
        }
        private void LoadImages()
        {
            var list = new List<RotatorModel>();
            foreach (var propertyCollectorImage in PropertyCollector.PropertyCollectorImages)
            {
                list.Add(new RotatorModel { Image = propertyCollectorImage.ImageUrl });
            }

            ImageCollection = new ObservableCollection<RotatorModel>(list);
        }
    }

}
