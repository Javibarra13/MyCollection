using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCollection.Common.Models;
using MyCollection.Web.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyCollection.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectorsController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public CollectorsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        [Route("GetCollectorByEmail")]
        public async Task<IActionResult> GetCollectorByEmailAsync(EmailRequest emailRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var collector = await _dataContext.Collectors
                .Include(c => c.User)
                .Include(c => c.PropertyCollectors)
                .ThenInclude(pc => pc.PropertyType)
                .Include(c => c.PropertyCollectors)
                .ThenInclude(pc => pc.PropertyCollectorImages)
                .FirstOrDefaultAsync(c => c.User.Email.ToLower() == emailRequest.Email.ToLower());

            if (collector == null)
            {
                return NotFound();
            }

            var response = new CollectorResponse
            {
                Id = collector.Id,
                FirstName = collector.User.FirstName,
                LastName = collector.User.LastName,
                Address = collector.User.Address,
                Document = collector.User.Document,
                Email = collector.User.Email,
                PhoneNumber = collector.User.PhoneNumber,
                PropertyCollectors = collector.PropertyCollectors?.Select(pc => new PropertyCollectorResponse
                {
                    Id = pc.Id,
                    Serie = pc.Serie,
                    Company = pc.Company,
                    Model = pc.Model,
                    Colour = pc.Colour,
                    Price = pc.Price,
                    IsAvailable = pc.IsAvailable,
                    Remarks = pc.Remarks,
                    PropertyCollectorImages = pc.PropertyCollectorImages?.Select(pci => new PropertyCollectorImageResponse
                    {
                        Id = pci.Id,
                        ImageUrl = pci.ImageFullPath
                    }).ToList(),
                    PropertyType = pc.PropertyType.Name,
                }).ToList()

            };

            return Ok(response);
        }
    }
}
