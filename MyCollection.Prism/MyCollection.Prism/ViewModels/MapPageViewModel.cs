using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyCollection.Prism.ViewModels
{
    public class MapPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public MapPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Mapa";
            _navigationService = navigationService;
        }
    }
}
