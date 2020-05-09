using Microsoft.AspNetCore.Mvc;
using MyCollection.Web.Data;
using MyCollection.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MyCollection.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyTypesController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public PropertyTypesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IEnumerable<PropertyType> GetPropertyTypes()
        {
            return _dataContext.PropertyTypes.OrderBy(pt => pt.Name);
        }
    }
}
