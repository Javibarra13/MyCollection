﻿using MyCollection.Common.Models;
using MyCollection.Common.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyCollection.Prism.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private string _password;
        private bool _isRunning;
        private bool _isEnabled;
        private DelegateCommand _loginCommand;

        public LoginPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            Title = "Login";
            IsEnabled = true;
            _navigationService = navigationService;
            _apiService = apiService;
        }

        public DelegateCommand LoginCommand => _loginCommand ?? (_loginCommand = new DelegateCommand(Login));

        public string Email { get; set; }

        public string Password { get => _password; set => SetProperty(ref _password, value); }

        public bool IsRunning { get => _isRunning; set => SetProperty(ref _isRunning, value); }

        public bool IsEnabled { get => _isEnabled; set => SetProperty(ref _isEnabled, value); }

        private async void Login()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debe ingrasar un email.", "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debe ingrasar una contraseña.", "Aceptar");
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            var request = new TokenRequest
            {
                Password = Password,
                Username = Email
            };

            var url = App.Current.Resources["UrlApi"].ToString();
            var response = await _apiService.GetTokenAsync(url, "/Account", "/CreateToken", request);

            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Email o contraseña son incorrectos.", "Aceptar");
                Password = string.Empty;
                return;
            }

            var token = response.Result;

            await App.Current.MainPage.DisplayAlert("Ok", "Fuck Yeah!!!.", "Aceptar");
        }
    }
}
