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
    public class CustomerPageViewModel : ViewModelBase
    {
        private CustomerResponse _customer;
        private ObservableCollection<RotatorModel> _imageCollection;
        private readonly INavigationService _navigationService;

        public CustomerPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Cliente";
            _navigationService = navigationService;
        }

        public ObservableCollection<RotatorModel> ImageCollection { get => _imageCollection; set => SetProperty(ref _imageCollection, value);}

        public CustomerResponse Customer { get => _customer; set => SetProperty(ref _customer, value); }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("customer"))
            {
                Customer = parameters.GetValue<CustomerResponse>("customer");
                Title = $"Cliente: {Customer.Name}";
                LoadImages();
            }
        }
        private void LoadImages()
        {
            var list = new List<RotatorModel>();
            foreach (var customerImage in Customer.CustomerImages)
            {
                list.Add(new RotatorModel { Image = customerImage.ImageUrl });
            }

            ImageCollection = new ObservableCollection<RotatorModel>(list);
        }
    }
}
