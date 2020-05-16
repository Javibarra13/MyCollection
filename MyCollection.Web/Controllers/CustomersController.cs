using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCollection.Web.Data;
using MyCollection.Web.Data.Entities;
using MyCollection.Web.Helpers;
using MyCollection.Web.Models;
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

        public IActionResult Index()
        {
            return View(_dataContext.Customers
                .Include(c => c.User)
                .Include(c => c.House)
                .Include(c => c.Collector)
                .ThenInclude(c => c.User));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _dataContext.Customers
                .Include(c => c.User)
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

        // GET: Customers/Create
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
                var user = await AddUser(viewModel);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "This email is already used.");
                    return View(viewModel);
                }

                var customer = new CustomerViewModel
                {
                    User = user,
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

        private async Task<User> AddUser(CustomerViewModel viewModel)
        {
            var user = new User
            {
                Address = viewModel.Address,
                Document = viewModel.Document,
                Email = viewModel.Username,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                PhoneNumber = viewModel.PhoneNumber,
                UserName = viewModel.Username
            };

            var result = await _userHelper.AddUserAsync(user, viewModel.Password);
            if (result != IdentityResult.Success)
            {
                return null;
            }

            var newUser = await _userHelper.GetUserByEmailAsync(viewModel.Username);
            await _userHelper.AddUserToRoleAsync(newUser, "Customer");
            return newUser;
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _dataContext.Customers
                .Include(c => c.User)
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
                Address = customer.User.Address,
                Document = customer.User.Document,
                FirstName = customer.User.FirstName,
                Id = customer.Id,
                LastName = customer.User.LastName,
                PhoneNumber = customer.User.PhoneNumber,
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
                    .Include(c => c.User)
                    .FirstOrDefaultAsync(o => o.Id == viewModel.Id);

                customer.User.Document = viewModel.Document;
                customer.House = await _dataContext.Houses.FindAsync(viewModel.HouseId);
                customer.Collector = await _dataContext.Collectors.FindAsync(viewModel.CollectorId);
                customer.User.FirstName = viewModel.FirstName;
                customer.User.LastName = viewModel.LastName;
                customer.User.Address = viewModel.Address;
                customer.User.PhoneNumber = viewModel.PhoneNumber;
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

                await _userHelper.UpdateUserAsync(customer.User);
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
                .Include(c => c.User)
                .Include(c => c.CustomerImages)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customers == null)
            {
                return NotFound();
            }

            _dataContext.CustomerImages.RemoveRange(customers.CustomerImages);
            _dataContext.Customers.Remove(customers);
            await _dataContext.SaveChangesAsync();
            await _userHelper.DeleteUserAsync(customers.User.Email);
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
