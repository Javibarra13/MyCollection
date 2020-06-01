using MyCollection.Web.Data.Entities;
using MyCollection.Web.Models;
using System.Threading.Tasks;

namespace MyCollection.Web.Helpers
{
    public interface IConverterHelper
    {
        Task<PropertyCollector> ToPropertyCollectorAsync(PropertyCollectorViewModel viewModel, bool isNew);
        PropertyCollectorViewModel ToPropertyCollectorViewModel(PropertyCollector propertyCollector);
        Task<PropertyManager> ToPropertyManagerAsync(PropertyManagerViewModel viewModel, bool isNew);
        PropertyManagerViewModel ToPropertyManagerViewModel(PropertyManager propertyManager);
        Task<PropertySeller> ToPropertySellerAsync(PropertySellerViewModel viewModel, bool isNew);
        PropertySellerViewModel ToPropertySellerViewModel(PropertySeller propertySeller);
        Task<PropertySupervisor> ToPropertySupervisorAsync(PropertySupervisorViewModel viewModel, bool isNew);
        PropertySupervisorViewModel ToPropertySupervisorViewModel(PropertySupervisor propertySupervisor);
        Task<Subline> ToSublineAsync(SublineViewModel viewModel, bool isNew);
        SublineViewModel ToSublineViewModel(Subline subline);
        Task<Product> ToProductAsync(ProductViewModel viewModel, bool isNew);
        ProductViewModel ToProductViewModel(Product product);
    }
}