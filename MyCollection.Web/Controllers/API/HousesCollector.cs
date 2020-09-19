using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCollection.Web.Data;
using MyCollection.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MyCollection.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class HousesController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public HousesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IEnumerable<House> GetHouses()
        {
            return _dataContext.Houses
                .Include(h => h.Customers)
                .OrderBy(pt => pt.Name);
        }
    }
}