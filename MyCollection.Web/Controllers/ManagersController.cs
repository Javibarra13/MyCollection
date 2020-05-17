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
    public class ManagersController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IImageHelper _imageHelper;

        public ManagersController(
            DataContext dataContext,
            IUserHelper userHelper,
            ICombosHelper combosHelper,
            IConverterHelper converterHelper,
            IImageHelper imageHelper)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
            _imageHelper = imageHelper;
        }

        public IActionResult Index()
        {
            return View(_dataContext.Managers
                .Include(m => m.User)
                .Include(m => m.PropertyManagers)
                .ToList());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _dataContext.Managers
                .Include(m => m.User)
                .Include(m => m.PropertyManagers)
                .ThenInclude(pm => pm.PropertyType)
                .Include(m => m.PropertyManagers)
                .ThenInclude(pm => pm.PropertyManagerImages)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manager == null)
            {
                return NotFound();
            }

            return View(manager);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddUserViewModel view)
        {
            if (ModelState.IsValid)
            {
                var user = await AddUser(view);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "This email is already used.");
                    return View(view);
                }

                var manager = new Manager
                {
                    PropertyManagers = new List<PropertyManager>(),
                    User = user,
                };

                _dataContext.Add(manager);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(view);
        }

        private async Task<User> AddUser(AddUserViewModel view)
        {
            var user = new User
            {
                Address = view.Address,
                Document = view.Document,
                Email = view.Username,
                FirstName = view.FirstName,
                LastName = view.LastName,
                PhoneNumber = view.PhoneNumber,
                UserName = view.Username
            };

            var result = await _userHelper.AddUserAsync(user, view.Password);
            if (result != IdentityResult.Success)
            {
                return null;
            }

            var newUser = await _userHelper.GetUserByEmailAsync(view.Username);
            await _userHelper.AddUserToRoleAsync(newUser, "Manager");
            return newUser;
        }

        public async Task<IActionResult> AddProperty(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _dataContext.Managers.FindAsync(id);
            if (manager == null)
            {
                return NotFound();
            }

            var model = new PropertyManagerViewModel
            {
                ManagerId = manager.Id,
                PropertyTypes = _combosHelper.GetComboPropertyTypes()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProperty(PropertyManagerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var propertyManager = await _converterHelper.ToPropertyManagerAsync(viewModel, true);
                _dataContext.PropertyManagers.Add(propertyManager);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction("Details", "Managers", new { id = viewModel.ManagerId });
            }

            viewModel.PropertyTypes = _combosHelper.GetComboPropertyTypes();

            return View(viewModel);
        }

        public async Task<IActionResult> EditProperty(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyManager = await _dataContext.PropertyManagers
                .Include(pc => pc.Manager)
                .Include(pc => pc.PropertyType)
                .FirstOrDefaultAsync(pc => pc.Id == id);
            if (propertyManager == null)
            {
                return NotFound();
            }

            var model = _converterHelper.ToPropertyManagerViewModel(propertyManager);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProperty(PropertyManagerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var propertyManager = await _converterHelper.ToPropertyManagerAsync(viewModel, false);
                _dataContext.PropertyManagers.Update(propertyManager);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction("Details", "Managers", new { id = viewModel.ManagerId });
            }

            return View(viewModel);
        }

        public async Task<IActionResult> DetailsProperty(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyManager = await _dataContext.PropertyManagers
                .Include(o => o.Manager)
                .ThenInclude(o => o.User)
                .Include(o => o.PropertyType)
                .Include(p => p.PropertyManagerImages)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (propertyManager == null)
            {
                return NotFound();
            }

            return View(propertyManager);
        }

        public async Task<IActionResult> DeleteProperty(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyManager = await _dataContext.PropertyManagers
                .Include(pc => pc.Manager)
                .Include(pc => pc.PropertyManagerImages)
                .FirstOrDefaultAsync(pc => pc.Id == id.Value);
            if (propertyManager == null)
            {
                return NotFound();
            }

            _dataContext.PropertyManagerImages.RemoveRange(propertyManager.PropertyManagerImages);
            _dataContext.PropertyManagers.Remove(propertyManager);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("Details", "Managers", new { id = propertyManager.Manager.Id });
        }

        public async Task<IActionResult> AddImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyManager = await _dataContext.PropertyManagers.FindAsync(id.Value);
            if (propertyManager == null)
            {
                return NotFound();
            }

            var model = new PropertyManagerImageViewModel
            {
                Id = propertyManager.Id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddImage(PropertyManagerImageViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if (viewModel.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(viewModel.ImageFile);
                }

                var propertyManagerImage = new PropertyManagerImage
                {
                    ImageUrl = path,
                    PropertyManager = await _dataContext.PropertyManagers.FindAsync(viewModel.Id)
                };

                _dataContext.PropertyManagerImages.Add(propertyManagerImage);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction("DetailsProperty", "Managers", new { id = viewModel.Id });
            }

            return View(viewModel);
        }

        public async Task<IActionResult> DeleteImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyManagerImage = await _dataContext.PropertyManagerImages
                .Include(pci => pci.PropertyManager)
                .FirstOrDefaultAsync(pci => pci.Id == id.Value);
            if (propertyManagerImage == null)
            {
                return NotFound();
            }

            _dataContext.PropertyManagerImages.Remove(propertyManagerImage);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("DetailsProperty", "Managers", new { id = propertyManagerImage.PropertyManager.Id });
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _dataContext.Managers
                .Include(c => c.User)
                .Include(c => c.PropertyManagers)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manager == null)
            {
                return NotFound();
            }

            if (manager.PropertyManagers.Count != 0)
            {
                ModelState.AddModelError(string.Empty, "Manager has related registers");
                return RedirectToAction(nameof(Index));
            }

            _dataContext.Managers.Remove(manager);
            await _dataContext.SaveChangesAsync();
            await _userHelper.DeleteUserAsync(manager.User.Email);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _dataContext.Managers
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == id.Value);
            if (manager == null)
            {
                return NotFound();
            }

            var model = new EditUserViewModel
            {
                Address = manager.User.Address,
                Document = manager.User.Document,
                FirstName = manager.User.FirstName,
                Id = manager.Id,
                LastName = manager.User.LastName,
                PhoneNumber = manager.User.PhoneNumber
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var manager = await _dataContext.Managers
                    .Include(c => c.User)
                    .FirstOrDefaultAsync(o => o.Id == viewModel.Id);

                manager.User.Document = viewModel.Document;
                manager.User.FirstName = viewModel.FirstName;
                manager.User.LastName = viewModel.LastName;
                manager.User.Address = viewModel.Address;
                manager.User.PhoneNumber = viewModel.PhoneNumber;

                await _userHelper.UpdateUserAsync(manager.User);
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        private bool ManagerExists(int id)
        {
            return _dataContext.Managers.Any(e => e.Id == id);
        }
    }
}
