using MyCollection.Web.Data.Entities;
using MyCollection.Web.Models;
using System.Threading.Tasks;

namespace MyCollection.Web.Helpers
{
    public interface IConverterHelper
    {
        Task<PropertyCollector> ToPropertyCollectorAsync(PropertyCollectorViewModel viewModel, bool isNew);
        PropertyCollectorViewModel ToPropertyCollectorViewModel(PropertyCollector propertyCollector);
    }
}