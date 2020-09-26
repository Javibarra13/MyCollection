using MyCollection.Common.Helpers;
using MyCollection.Common.Models;
using Newtonsoft.Json;
using Prism.Navigation;
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
            Title = "Detalles";
            _navigationService = navigationService;
        }

        public ObservableCollection<RotatorModel> ImageCollection { get => _imageCollection; set => SetProperty(ref _imageCollection, value); }

        public CustomerResponse Customer { get => _customer; set => SetProperty(ref _customer, value); }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            Customer = JsonConvert.DeserializeObject<CustomerResponse>(Settings.Customer);
            LoadImages();
        }

        private void LoadImages()
        {
            ImageCollection = new ObservableCollection<RotatorModel>(_customer.CustomerImages.Select(ci => new RotatorModel
            {
                Image = ci.ImageUrl
            }).ToList());
        }
    }
}
