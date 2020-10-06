using MyCollection.Common.Helpers;
using MyCollection.Common.Models;
using Newtonsoft.Json;
using Prism.Navigation;

namespace MyCollection.Prism.ViewModels
{
    public class SaleDetailsTabbedPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public SaleDetailsTabbedPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            var sale = JsonConvert.DeserializeObject<SaleResponse>(Settings.Sale);
            Title = $"Venta: {sale.Id}";
            _navigationService = navigationService;
        }
    }
}
