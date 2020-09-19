using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCollection.Web.Data;
using MyCollection.Web.Data.Entities;
using MyCollection.Web.Extensions;
using MyCollection.Web.Helpers;
using MyCollection.Web.Models;
using RandomGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Vereyon.Web;

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
        private readonly IFlashMessage _flashMessage;

        public CustomersController(
            DataContext dataContext,
            IImageHelper imageHelper,
            IUserHelper userHelper,
            ICombosHelper combosHelper,
            IConverterHelper converterHelper,
            IFlashMessage flashMessage)
        {
            _dataContext = dataContext;
            _imageHelper = imageHelper;
            _userHelper = userHelper;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
            _flashMessage = flashMessage;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoadData()
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();

                var start = Request.Form["start"].FirstOrDefault();

                var length = Request.Form["length"].FirstOrDefault();

                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();

                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();

                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;

                int skip = start != null ? Convert.ToInt32(start) : 0;

                int recordsTotal = 0;

                var customerData = _dataContext.Customers.AsQueryable();

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    customerData = customerData.OrderBy(sortColumn + " " + sortColumnDirection);
                }

                if (!string.IsNullOrEmpty(searchValue))
                {
                    customerData = customerData.Where(m => m.Name.Contains(searchValue) ||
                                                           m.Address.Contains(searchValue) ||
                                                           m.Neighborhood.Contains(searchValue) ||
                                                           m.Id.ToString().Contains(searchValue)).AsQueryable();
                }

                recordsTotal = customerData.Count();

                var data = customerData.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception)
            {
                throw;
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
                var customer = await _dataContext.Customers
                    .Include(c => c.House)
                    .Include(c => c.Collector)
                    .ThenInclude(c => c.User)
                    .FirstOrDefaultAsync(o => o.Id == viewModel.Id);

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

                await _dataContext.SaveChangesAsync();
                _flashMessage.Info("Registro editado con éxito.");

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
                .Include(c => c.Sales)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (customers == null)
            {
                return NotFound();
            }

            if (customers.Sales.Count != 0)
            {
                _flashMessage.Danger("Registro no pudo ser eliminado, cuenta con registros relacionados.");
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _dataContext.CustomerImages.RemoveRange(customers.CustomerImages);
                _dataContext.Customers.Remove(customers);
                await _dataContext.SaveChangesAsync();
                _flashMessage.Confirmation("Registro eliminado con éxito.");
            }
            catch
            {

                _flashMessage.Danger("Registro no pudo ser eliminado, cuenta con registros relacionados.");
            }
            
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
            return RedirectToAction("Details", "Customers", new { id = customerImage.Customer.Id });
        }

        private bool CustomerExists(int id)
        {
            return _dataContext.Customers.Any(e => e.Id == id);
        }
    }
}
