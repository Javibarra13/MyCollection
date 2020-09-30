using Prism;
using Prism.Ioc;
using MyCollection.Prism.ViewModels;
using MyCollection.Prism.Views;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using MyCollection.Common.Services;
using Newtonsoft.Json;
using MyCollection.Common.Helpers;
using MyCollection.Common.Models;
using System;

namespace MyCollection.Prism
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzE5Nzk1QDMxMzgyZTMyMmUzMGwrV0pvYjFlSXY1WmFaNTMrU1lEVzNGWGJJYUJiMElBVUxnWHBzTllDeVk9");

            InitializeComponent();

            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            if (Settings.IsRemember && token?.Expiration > DateTime.Now)
            {
                await NavigationService.NavigateAsync("/CollectionMasterDetailPage/NavigationPage/CustomersPage");
            }
            else

            {
                await NavigationService.NavigateAsync("/NavigationPage/LoginPage");
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.Register<IGeolocatorService, GeolocatorService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<SalesPage, SalesPageViewModel>();
            containerRegistry.RegisterForNavigation<CustomersPage, CustomersPageViewModel>();
            containerRegistry.RegisterForNavigation<CustomerPage, CustomerPageViewModel>();
            containerRegistry.RegisterForNavigation<SalePage, SalePageViewModel>();
            containerRegistry.RegisterForNavigation<DetailsTabbedPage, DetailsTabbedPageViewModel>();
            containerRegistry.RegisterForNavigation<CollectionMasterDetailPage, CollectionMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<ModifyUserPage, ModifyUserPageViewModel>();
            containerRegistry.RegisterForNavigation<MapPage, MapPageViewModel>();
            containerRegistry.RegisterForNavigation<RememberPasswordPage, RememberPasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<ChangePasswordPage, ChangePasswordPageViewModel>();
        }
    }
}
