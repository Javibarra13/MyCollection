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
    public class SalePageViewModel : ViewModelBase
    {
        private SaleResponse _sale;
        private ObservableCollection<RotatorModel> _imageCollection;

        public SalePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Sale";
        }

        public ObservableCollection<RotatorModel> ImageCollection
        {
            get => _imageCollection;
            set => SetProperty(ref _imageCollection, value);
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
                Title = $"Detalles de: {Sale.Customer.FullName}";
                LoadImages();
            }
        }
        private void LoadImages()
        {
            var list = new List<RotatorModel>();
            foreach (var customerImage in Sale.Customer.CustomerImages)
            {
                list.Add(new RotatorModel { Image = customerImage.ImageUrl });
            }

            ImageCollection = new ObservableCollection<RotatorModel>(list);
        }
    }
}
