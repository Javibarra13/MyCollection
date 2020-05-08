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

        public CollectorsController(
            DataContext dataContext,
            IUserHelper userHelper,
            ICombosHelper combosHelper,
            IConverterHelper converterHelper)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
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

        public async Task<IActionResult> Edit(int? id)
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
            return View(collector);
        }

        // POST: Collectors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] Collector collector)
        {
            if (id != collector.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dataContext.Update(collector);
                    await _dataContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollectorExists(collector.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(collector);
        }

        // GET: Collectors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collector = await _dataContext.Collectors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collector == null)
            {
                return NotFound();
            }

            return View(collector);
        }

        // POST: Collectors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var collector = await _dataContext.Collectors.FindAsync(id);
            _dataContext.Collectors.Remove(collector);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CollectorExists(int id)
        {
            return _dataContext.Collectors.Any(e => e.Id == id);
        }
    }
}
