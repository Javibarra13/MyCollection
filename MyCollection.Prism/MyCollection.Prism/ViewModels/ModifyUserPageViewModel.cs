using MyCollection.Common.Helpers;
using MyCollection.Common.Models;
using MyCollection.Common.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCollection.Prism.ViewModels
{
    public class ModifyUserPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private bool _isRunning;
        private bool _isEnabled;
        private CollectorResponse _collector;
        private DelegateCommand _saveCommand;
        private DelegateCommand _changePasswordCommand;

        public ModifyUserPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            Title = "Modificar Usuario";
            _isEnabled = true;
            Collector = JsonConvert.DeserializeObject<CollectorResponse>(Settings.Collector);
            _navigationService = navigationService;
            _apiService = apiService;
        }

        public DelegateCommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(SaveAsync));

        public DelegateCommand ChangePasswordCommand => _changePasswordCommand ?? (_changePasswordCommand = new DelegateCommand(ChangePasswordAsync));

        public CollectorResponse Collector { get => _collector; set => SetProperty(ref _collector, value); }

        public bool IsRunning { get => _isRunning; set => SetProperty(ref _isRunning, value); }

        public bool IsEnabled { get => _isEnabled; set => SetProperty(ref _isEnabled, value); }

        private async void SaveAsync()
        {
            var isValid = await ValidateDataAsync();
            if (!isValid)
            {
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            var userRequest = new UserRequest
            {
                Address = Collector.Address,
                Document = Collector.Document,
                Email = Collector.Email,
                FirstName = Collector.FirstName,
                LastName = Collector.LastName,
                Password = "123456", // It doesn't matter what is sent here. It is only for the model to be valid
                Phone = Collector.PhoneNumber
            };

            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            var url = App.Current.Resources["UrlAPI"].ToString();
            var response = await _apiService.PutAsync(
                url,
                "/api",
                "/Account",
                userRequest,
                "bearer",
                token.Token);

            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            Settings.Collector = JsonConvert.SerializeObject(Collector);

            await App.Current.MainPage.DisplayAlert(
                "Ok",
                "Usuario actualizado de forma correcta.",
                "Accept");

        }

        private async Task<bool> ValidateDataAsync()
        {
            if (string.IsNullOrEmpty(Collector.Document))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debe ingresar un documento.", "Aceptar");
                return false;
            }

            if (string.IsNullOrEmpty(Collector.FirstName))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debe ingresar nombres.", "Aceptar");
                return false;
            }

            if (string.IsNullOrEmpty(Collector.LastName))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debe ingresar apellidos.", "Aceptar");
                return false;
            }

            if (string.IsNullOrEmpty(Collector.Address))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debe ingresar una dirección.", "Aceptar");
                return false;
            }

            return true;
        }

        private async void ChangePasswordAsync()
        {
            await _navigationService.NavigateAsync("ChangePasswordPage");
        }
    }
}
