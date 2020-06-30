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
        private string _password;
        private bool _isRunning;
        private bool _isEnabled;

        public LoginPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Login";
        }

        public string Email { get; set; }

        public string Password { get => _password; set => SetProperty(ref _password, value); }

        public bool IsRunning { get => _isRunning; set => SetProperty(ref _isRunning, value); }

        public bool IsEnabled { get => _isEnabled; set => SetProperty(ref _isEnabled, value); }
    }
}
