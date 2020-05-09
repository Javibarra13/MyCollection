using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCollection.Web.Data;
using MyCollection.Web.Data.Entities;
using MyCollection.Web.Helpers;
using MyCollection.Web.Models;

namespace MyCollection.Web.Controllers
{
    public class CollectorsController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IImageHelper _imageHelper;

        public CollectorsController(
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
            return View(_dataContext.Collectors
                .Include(c => c.User)
                .Include(c => c.PropertyCollectors));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collector = await _dataContext.Collectors
                .Include(c => c.User)
                .Include(c => c.PropertyCollectors)
                .ThenInclude(pc => pc.PropertyType)
                .Include(c => c.PropertyCollectors)
                .ThenInclude(pc => pc.PropertyCollectorImages)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collector == null)
            {
                return NotFound();
            }

            return View(collector);
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

                var collector = new Collector
                {
                    PropertyCollectors = new List<PropertyCollector>(),
                    User = user,
                };

                _dataContext.Collectors.Add(collector);
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
            await _userHelper.AddUserToRoleAsync(newUser, "Collector");
            return newUser;
        }

        public async Task<IActionResult> AddProperty(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collector = await _dataContext.Collectors.FindAsync(id);
            if (collector == null)
            {
                return NotFound();
            }
            var model = new PropertyCollectorViewModel
            {
                CollectorId = collector.Id,
                PropertyTypes = _combosHelper.GetComboPropertyTypes()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProperty(PropertyCollectorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var propertyCollector = await _converterHelper.ToPropertyCollectorAsync(viewModel, true);
                _dataContext.PropertyCollectors.Add(propertyCollector);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction("Details","Collectors", new { id = viewModel.CollectorId });
            }
            return View(viewModel);
        }

        public async Task<IActionResult> EditProperty(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyCollector = await _dataContext.PropertyCollectors
                .Include(pc => pc.Collector)
                .Include(pc => pc.PropertyType)
                .FirstOrDefaultAsync(pc => pc.Id == id);
            if (propertyCollector == null)
            {
                return NotFound();
            }

            var model = _converterHelper.ToPropertyCollectorViewModel(propertyCollector);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProperty(PropertyCollectorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var propertyCollector = await _converterHelper.ToPropertyCollectorAsync(viewModel, false);
                _dataContext.PropertyCollectors.Update(propertyCollector);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction("Details", "Collectors", new { id = viewModel.CollectorId });
            }

            return View(viewModel);
        }

        public async Task<IActionResult> DetailsProperty(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyCollector = await _dataContext.PropertyCollectors
                .Include(o => o.Collector)
                .ThenInclude(o => o.User)
                .Include(o => o.PropertyType)
                .Include(p => p.PropertyCollectorImages)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (propertyCollector == null)
            {
                return NotFound();
            }

            return View(propertyCollector);
        }

        public async Task<IActionResult> AddImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyCollector = await _dataContext.PropertyCollectors.FindAsync(id.Value);
            if (propertyCollector == null)
            {
                return NotFound();
            }

            var model = new PropertyCollectorImageViewModel
            {
                Id = propertyCollector.Id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddImage(PropertyCollectorImageViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if (viewModel.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(viewModel.ImageFile);
                }

                var propertyCollectorImage = new PropertyCollectorImage
                {
                    ImageUrl = path,
                    PropertyCollector = await _dataContext.PropertyCollectors.FindAsync(viewModel.Id)
                };

                _dataContext.PropertyCollectorImages.Add(propertyCollectorImage);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction("DetailsProperty", "Collectors", new { id = viewModel.Id });
            }

            return View(viewModel);
        }

        public async Task<IActionResult> DeleteImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyCollectorImage = await _dataContext.PropertyCollectorImages
                .Include(pci => pci.PropertyCollector)
                .FirstOrDefaultAsync(pci => pci.Id == id.Value);
            if (propertyCollectorImage == null)
            {
                return NotFound();
            }

            _dataContext.PropertyCollectorImages.Remove(propertyCollectorImage);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("DetailsProperty", "Collectors", new { id = propertyCollectorImage.PropertyCollector.Id });
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collector = await _dataContext.Collectors
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == id.Value);
            if (collector == null)
            {
                return NotFound();
            }

            var model = new EditUserViewModel
            {
                Address = collector.User.Address,
                Document = collector.User.Document,
                FirstName = collector.User.FirstName,
                Id = collector.Id,
                LastName = collector.User.LastName,
                PhoneNumber = collector.User.PhoneNumber
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var collector = await _dataContext.Collectors
                    .Include(c => c.User)
                    .FirstOrDefaultAsync(o => o.Id == viewModel.Id);

                collector.User.Document = viewModel.Document;
                collector.User.FirstName = viewModel.FirstName;
                collector.User.LastName = viewModel.LastName;
                collector.User.Address = viewModel.Address;
                collector.User.PhoneNumber = viewModel.PhoneNumber;

                await _userHelper.UpdateUserAsync(collector.User);
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }


        // GET: Collectors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collector = await _dataContext.Collectors
                .Include(c => c.User)
                .Include(c => c.PropertyCollectors)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collector == null)
            {
                return NotFound();
            }

            if (collector.PropertyCollectors.Count != 0)
            {
                ModelState.AddModelError(string.Empty, "Collector has related registers");
                return RedirectToAction(nameof(Index));
            }

            _dataContext.Collectors.Remove(collector);
            await _dataContext.SaveChangesAsync();
            await _userHelper.DeleteUserAsync(collector.User.Email);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteProperty(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyCollector = await _dataContext.PropertyCollectors
                .Include(pc => pc.Collector)
                .Include(pc => pc.PropertyCollectorImages)
                .FirstOrDefaultAsync(pc => pc.Id == id.Value);
            if (propertyCollector == null)
            {
                return NotFound();
            }

            _dataContext.PropertyCollectorImages.RemoveRange(propertyCollector.PropertyCollectorImages);
            _dataContext.PropertyCollectors.Remove(propertyCollector);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("Details", "Collectors", new { id = propertyCollector.Collector.Id });
        }



        private bool CollectorExists(int id)
        {
            return _dataContext.Collectors.Any(e => e.Id == id);
        }
    }
}
