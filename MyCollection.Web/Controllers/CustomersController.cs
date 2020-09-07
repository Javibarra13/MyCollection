using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCollection.Web.Data;
using MyCollection.Web.Data.Entities;
using MyCollection.Web.Extensions;
using MyCollection.Web.Helpers;
using MyCollection.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCollection.Web.Controllers
{
    [Authorize(Roles = "Manager")]
    public class CustomersController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IImageHelper _imageHelper;
        private readonly IUserHelper _userHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;

        public CustomersController(
            DataContext dataContext,
            IImageHelper imageHelper,
            IUserHelper userHelper,
            ICombosHelper combosHelper,
            IConverterHelper converterHelper)
        {
            _dataContext = dataContext;
            _imageHelper = imageHelper;
            _userHelper = userHelper;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
        }

        //public IActionResult Index()
        public async Task<IActionResult> Index()
        {
            await SeedData();

            //return View(_dataContext.Customers
            //    .Include(c => c.House)
            //    .Include(c => c.Collector)
            //    .ThenInclude(c => c.User));

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoadTable([FromBody]DtParameters dtParameters)
        {
            var searchBy = dtParameters.Search?.Value;

            // if we have an empty search then just order the results by Id ascending
            var orderCriteria = "Id";
            var orderAscendingDirection = true;

            if (dtParameters.Order != null)
            {
                // in this example we just default sort on the 1st column
                orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
            }

            var result = _dataContext.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.Name != null && r.Name.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.Address != null && r.Address.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.Neighborhood != null && r.Neighborhood.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.City != null && r.City.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.PhoneNumber != null && r.PhoneNumber.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.Status != null && r.Status.ToUpper().Contains(searchBy.ToUpper()));
            }

            result = orderAscendingDirection ? result.OrderByDynamic(orderCriteria, DtOrderDir.Asc) : result.OrderByDynamic(orderCriteria, DtOrderDir.Desc);

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            var filteredResultsCount = await result.CountAsync();
            var totalResultsCount = await _dataContext.Customers.CountAsync();

            return Json(new DtResult<Customer>
            {
                Draw = dtParameters.Draw,
                RecordsTotal = totalResultsCount,
                RecordsFiltered = filteredResultsCount,
                Data = await result
                    .Skip(dtParameters.Start)
                    .Take(dtParameters.Length)
                    .ToListAsync()
            });
        }

        public async Task SeedData()
        {
            if (!_dataContext.Customers.Any())
            {
                for (var i = 0; i < 1000; i++)
                {
                    await _dataContext.Customers.AddAsync(new Customer
                    {
                        Name = i % 2 == 0 ? Gen.Random.Names.Male()() : Gen.Random.Names.Female()(),
                        FirstSurname = Gen.Random.Names.Surname()(),
                        SecondSurname = Gen.Random.Names.Surname()(),
                        Street = Gen.Random.Names.Full()(),
                        Phone = Gen.Random.PhoneNumbers.WithRandomFormat()(),
                        ZipCode = Gen.Random.Numbers.Integers(10000, 99999)().ToString(),
                        Country = Gen.Random.Countries()(),
                        Notes = Gen.Random.Text.Short()(),
                        CreationDate = Gen.Random.Time.Dates(DateTime.Now.AddYears(-100), DateTime.Now)()
                    });
                }

                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _dataContext.Customers
                .Include(c => c.House)
                .Include(c => c.CustomerImages)
                .Include(c => c.Collector)
                .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        public async Task<IActionResult> AddImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _dataContext.Customers.FindAsync(id.Value);
            if (customer == null)
            {
                return NotFound();
            }

            var model = new CustomerImageViewModel
            {
                Id = customer.Id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddImage(CustomerImageViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if (viewModel.ImageFile != null)
                {
                    path = await _imageHelper.UploadCustomerImageAsync(viewModel.ImageFile);
                }

                var customerImage = new CustomerImage
                {
                    ImageUrl = path,
                    Customer = await _dataContext.Customers.FindAsync(viewModel.Id)
                };

                _dataContext.CustomerImages.Add(customerImage);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction("Details", "Customers", new { id = viewModel.Id });
            }

            return View(viewModel);
        }

        public IActionResult Create()
        {
            var model = new CustomerViewModel
            {
                Collectors = _combosHelper.GetComboCollectors(),
                Houses = _combosHelper.GetComboHouses(),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var customer = new CustomerViewModel
                {
                    Document = viewModel.Document,
                    Name = viewModel.Name,
                    Address = viewModel.Address,
                    PhoneNumber = viewModel.PhoneNumber,
                    City = viewModel.City,
                    House = await _dataContext.Houses.FindAsync(viewModel.HouseId),
                    Collector = await _dataContext.Collectors.FindAsync(viewModel.CollectorId),
                    Neighborhood = viewModel.Neighborhood,
                    PostalCode = viewModel.PostalCode,
                    RefName = viewModel.RefName,
                    RefAddress = viewModel.RefAddress,
                    RefPhone = viewModel.RefPhone,
                    RefName2 = viewModel.RefName2,
                    RefAddress2 = viewModel.RefAddress2,
                    RefPhone2 = viewModel.RefPhone2,
                    Remarks = viewModel.Remarks,
                    Status = viewModel.Status,
                    CustomerImages = new List<CustomerImage>(),
                };

                _dataContext.Customers.Add(customer);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            viewModel.Houses = _combosHelper.GetComboHouses();
            viewModel.Collectors = _combosHelper.GetComboCollectors();

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _dataContext.Customers
                .Include(c => c.Collector)
                .ThenInclude(c => c.User)
                .Include(c => c.House)
                .FirstOrDefaultAsync(c => c.Id == id.Value);
            if (customer == null)
            {
                return NotFound();
            }

            var model = new EditCustomerViewModel
            {
                Address = customer.Address,
                Document = customer.Document,
                Name = customer.Name,
                Id = customer.Id,
                PhoneNumber = customer.PhoneNumber,
                City = customer.City,
                Neighborhood = customer.Neighborhood,
                PostalCode = customer.PostalCode,
                RefName = customer.RefName,
                RefAddress = customer.RefAddress,
                RefPhone = customer.RefPhone,
                RefName2 = customer.RefName2,
                RefAddress2 = customer.RefAddress2,
                RefPhone2 = customer.RefPhone2,
                Remarks = customer.Remarks,
                Status = customer.Status,
                HouseId = customer.House.Id,
                CollectorId = customer.Collector.Id,
            };

            model.Houses = _combosHelper.GetComboHouses();
            model.Collectors = _combosHelper.GetComboCollectors();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditCustomerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var customer = await _dataContext.Customers.FirstOrDefaultAsync(o => o.Id == viewModel.Id);

                customer.Document = viewModel.Document;
                customer.House = await _dataContext.Houses.FindAsync(viewModel.HouseId);
                customer.Collector = await _dataContext.Collectors.FindAsync(viewModel.CollectorId);
                customer.Name = viewModel.Name;
                customer.Address = viewModel.Address;
                customer.PhoneNumber = viewModel.PhoneNumber;
                customer.City = viewModel.City;
                customer.Neighborhood = viewModel.Neighborhood;
                customer.PostalCode = viewModel.PostalCode;
                customer.RefName = viewModel.RefName;
                customer.RefAddress = viewModel.RefAddress;
                customer.RefPhone = viewModel.RefPhone;
                customer.RefName2 = viewModel.RefName2;
                customer.RefAddress2 = viewModel.RefAddress2;
                customer.RefPhone2 = viewModel.RefPhone2;
                customer.Remarks = viewModel.Remarks;
                customer.Status = viewModel.Status;

                return RedirectToAction("Details", "Customers", new { id = customer.Id });
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customers = await _dataContext.Customers
                .Include(c => c.CustomerImages)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customers == null)
            {
                return NotFound();
            }

            _dataContext.CustomerImages.RemoveRange(customers.CustomerImages);
            _dataContext.Customers.Remove(customers);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerImage = await _dataContext.CustomerImages
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(pci => pci.Id == id.Value);
            if (customerImage == null)
            {
                return NotFound();
            }

            _dataContext.CustomerImages.Remove(customerImage);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("Details", "Customers", new { id = customerImage.Id });
        }

        private bool CustomerExists(int id)
        {
            return _dataContext.Customers.Any(e => e.Id == id);
        }
    }
}
