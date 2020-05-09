using Prism;
using Prism.Ioc;
using MyCollection.Prism.ViewModels;
using MyCollection.Prism.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyCollection.Common.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MyCollection.Prism
{
    public partial class App
    {
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/LoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<PropertyCollectorsPage, PropertyCollectorsPageViewModel>();
        }
    }
}
