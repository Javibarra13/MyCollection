using Prism;
using Prism.Ioc;
using MyCollection.Prism.ViewModels;
using MyCollection.Prism.Views;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyCollection.Common.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MyCollection.Prism
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mjc5NDY2QDMxMzgyZTMxMmUzMGJ4RnlxU2JkMzhtbXhHalVnOTYwa3VTUEhoclRya2o1S2NhcDF0cDBHWFE9");

            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/LoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<PropertiesPage, PropertiesPageViewModel>();
            containerRegistry.RegisterForNavigation<PropertyCollectorPage, PropertyCollectorPageViewModel>();
            containerRegistry.RegisterForNavigation<SalesPage, SalesPageViewModel>();
            containerRegistry.RegisterForNavigation<SalePage, SalePageViewModel>();
            containerRegistry.RegisterForNavigation<SaleDetailsPage, SaleDetailsPageViewModel>();
        }
    }
}
