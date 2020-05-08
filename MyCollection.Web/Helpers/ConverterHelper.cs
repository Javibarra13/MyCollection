using MyCollection.Web.Data;
using MyCollection.Web.Data.Entities;
using MyCollection.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCollection.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _dataContext;
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(
            DataContext dataContext,
            ICombosHelper combosHelper)
        {
            _dataContext = dataContext;
            _combosHelper = combosHelper;
        }

        public CombosHelper CombosHelper { get; }

        public async Task<PropertyCollector> ToPropertyCollectorAsync(PropertyCollectorViewModel viewModel, bool isNew)
        {
            return new PropertyCollector
            {
                Id = isNew ? 0 : viewModel.Id,
                Serie = viewModel.Serie,
                Company = viewModel.Company,
                Model = viewModel.Model,
                Colour = viewModel.Colour,
                IsAvailable = viewModel.IsAvailable,
                Price = viewModel.Price,
                Collector = await _dataContext.Collectors.FindAsync(viewModel.CollectorId),
                PropertyType = await _dataContext.PropertyTypes.FindAsync(viewModel.PropertyTypeId),
                PropertyCollectorImages = isNew ? new List<PropertyCollectorImage>() : viewModel.PropertyCollectorImages,
                Remarks = viewModel.Remarks
            };
        }

        public PropertyCollectorViewModel ToPropertyCollectorViewModel(PropertyCollector propertyCollector)
        {
            return new PropertyCollectorViewModel
            {
                Id = propertyCollector.Id,
                Serie = propertyCollector.Serie,
                Company = propertyCollector.Company,
                Model = propertyCollector.Model,
                Colour = propertyCollector.Colour,
                IsAvailable = propertyCollector.IsAvailable,
                Price = propertyCollector.Price,
                Collector = propertyCollector.Collector,
                PropertyType = propertyCollector.PropertyType,
                PropertyCollectorImages = propertyCollector.PropertyCollectorImages,
                Remarks = propertyCollector.Remarks,
                CollectorId = propertyCollector.Collector.Id,
                PropertyTypeId = propertyCollector.PropertyType.Id,
                PropertyTypes = _combosHelper.GetComboPropertyTypes()
            };
        }
    }
}
