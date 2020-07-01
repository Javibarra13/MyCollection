using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCollection.Common.Models;
using MyCollection.Web.Data;
using MyCollection.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace MyCollection.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
                .Include(c => c.Customers)
                .ThenInclude(c => c.User)
                .Include(c => c.Sales)
                .ThenInclude(s => s.Warehouse)
                .Include(c => c.Sales)
                .ThenInclude(s => s.House)
                .Include(c => c.Sales)
                .ThenInclude(s => s.Collector)
                .ThenInclude(c => c.User)
                .Include(c => c.Sales)
                .ThenInclude(s => s.TypePayment)
                .Include(c => c.Sales)
                .ThenInclude(s => s.DayPayment)
                .Include(c => c.Sales)
                .ThenInclude(s => s.Seller)
                .ThenInclude(s => s.User)
                .Include(c => c.Sales)
                .ThenInclude(s => s.Customer)
                .ThenInclude(c => c.User)
                .Include(c => c.Sales)
                .ThenInclude(s => s.State)
                .Include(c => c.Payments)
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
                Sales = collector.Sales?.Select(s => new SaleResponse
                { 
                    Id = s.Id,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    Payment = s.Payment,
                    Deposit = s.Deposit,
                    Remarks = s.Remarks,
                    Warehouse = ToWarehouseResponse(s.Warehouse),
                    House = ToHouseResponse(s.House),
                    Collector = ToCollectorResponse(s.Collector),
                    TypePayment = ToTypePaymentResponse(s.TypePayment),
                    DayPayment = ToDayPaymentResponse(s.DayPayment),
                    Seller = ToSellerResponse(s.Seller),
                    Customer = ToCustomerResponse(s.Customer),
                    State = ToStateResponse(s.State),
                }).ToList(),
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
        private WarehouseResponse ToWarehouseResponse(Warehouse warehouse)
        {
            return new WarehouseResponse
            {
                Id = warehouse.Id,
                Name = warehouse.Name
            };
        }

        private HouseResponse ToHouseResponse(House house)
        {
            return new HouseResponse
            {
                Id = house.Id,
                Name = house.Name
            };
        }

        private CollectorResponse ToCollectorResponse(Collector collector)
        {
            return new CollectorResponse
            {
                Id = collector.Id,
                Address = collector.User.Address,
                Document = collector.User.Document,
                Email = collector.User.Email,
                FirstName = collector.User.FirstName,
                LastName = collector.User.LastName,
                PhoneNumber = collector.User.PhoneNumber
            };
        }

        private TypePaymentResponse ToTypePaymentResponse(TypePayment typePayment)
        {
            return new TypePaymentResponse
            {
                Id = typePayment.Id,
                Name = typePayment.Name
            };
        }

        private DayPaymentResponse ToDayPaymentResponse(DayPayment dayPayment)
        {
            return new DayPaymentResponse
            {
                Id = dayPayment.Id,
                Name = dayPayment.Name
            };
        }

        private SellerResponse ToSellerResponse(Seller seller)
        {
            return new SellerResponse
            {
                Id = seller.Id,
                Address = seller.User.Address,
                Document = seller.User.Document,
                Email = seller.User.Email,
                FirstName = seller.User.FirstName,
                LastName = seller.User.LastName,
                PhoneNumber = seller.User.PhoneNumber
            };
        }

        private CustomerResponse ToCustomerResponse(Customer customer)
        {
            return new CustomerResponse
            {
                Id = customer.Id,
                Address = customer.User.Address,
                Document = customer.User.Document,
                Email = customer.User.Email,
                FirstName = customer.User.FirstName,
                LastName = customer.User.LastName,
                PhoneNumber = customer.User.PhoneNumber
            };
        }

        private StateResponse ToStateResponse(State state)
        {
            return new StateResponse
            {
                Id = state.Id,
                Name = state.Name
            };
        }
    }
}
