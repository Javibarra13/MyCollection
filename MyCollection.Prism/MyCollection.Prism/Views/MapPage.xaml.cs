using MyCollection.Common.Helpers;
using MyCollection.Common.Models;
using MyCollection.Common.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MyCollection.Prism.Views
{
    public partial class MapPage : ContentPage
    {
        private readonly IGeolocatorService _geolocatorService;
        private readonly IApiService _apiService;

        public MapPage(
                    IGeolocatorService geolocatorService,
                    IApiService apiService)
        {
            InitializeComponent();
            _geolocatorService = geolocatorService;
            _apiService = apiService;
            ShowCustomersAsync();
            MoveMapToCurrentPositionAsync();
        }

        private async void ShowCustomersAsync()
        {
            var url = App.Current.Resources["UrlApi"].ToString();
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            var response = await _apiService.GetListAsync<CustomerResponse>(url, "api", "/Collectors/GetAvailbleCustomers", "bearer", token.Token);

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }

            var customers = (List<CustomerResponse>)response.Result;

            foreach (var customer in customers)
            {
                MyMap.Pins.Add(new Pin
                {
                    Address = customer.Address,
                    Label = customer.Name,
                    Position = new Position(customer.Latitude, customer.Longitude),
                    Type = PinType.Place
                });
            }
        }

        private async void MoveMapToCurrentPositionAsync()
        {
            await _geolocatorService.GetLocationAsync();
            if (_geolocatorService.Latitude != 0 && _geolocatorService.Longitude != 0)
            {
                var position = new Position(
                    _geolocatorService.Latitude,
                    _geolocatorService.Longitude);
                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(
                    position,
                    Distance.FromKilometers(.5)));
            }
        }
    }
}
