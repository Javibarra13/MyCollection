using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCollection.Web.Data;
using MyCollection.Web.Data.Entities;

namespace MyCollection.Web.Controllers
{
    public class PropertyTypesController : Controller
    {
        private readonly DataContext _dataContext;

        public PropertyTypesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // GET: PropertyTypes
        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.PropertyTypes.ToListAsync());
        }

        // GET: PropertyTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyType = await _dataContext.PropertyTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (propertyType == null)
            {
                return NotFound();
            }

            return View(propertyType);
        }

        // GET: PropertyTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PropertyTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] PropertyType propertyType)
        {
            if (ModelState.IsValid)
            {
                _dataContext.Add(propertyType);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(propertyType);
        }

        // GET: PropertyTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyType = await _dataContext.PropertyTypes.FindAsync(id);
            if (propertyType == null)
            {
                return NotFound();
            }
            return View(propertyType);
        }

        // POST: PropertyTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] PropertyType propertyType)
        {
            if (id != propertyType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dataContext.Update(propertyType);
                    await _dataContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertyTypeExists(propertyType.Id))
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
            return View(propertyType);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyTypes = await _dataContext.PropertyTypes
                .Include(pt => pt.PropertyCollectors)
                .Include(pt => pt.PropertyManagers)
                .Include(pt => pt.PropertySellers)
                .Include(pt => pt.PropertySupervisors)
                .FirstOrDefaultAsync(pt => pt.Id == id);
            if (propertyTypes == null)
            {
                return NotFound();
            }

            if (propertyTypes.PropertyCollectors.Count > 0)
            {
                ModelState.AddModelError(string.Empty, "Register has related records");
                return RedirectToAction(nameof(Index));
            }
            if (propertyTypes.PropertyManagers.Count > 0)
            {
                ModelState.AddModelError(string.Empty, "Register has related records");
                return RedirectToAction(nameof(Index));
            }
            if (propertyTypes.PropertySellers.Count > 0)
            {
                ModelState.AddModelError(string.Empty, "Register has related records");
                return RedirectToAction(nameof(Index));
            }
            if (propertyTypes.PropertySupervisors.Count > 0)
            {
                ModelState.AddModelError(string.Empty, "Register has related records");
                return RedirectToAction(nameof(Index));
            }

            _dataContext.PropertyTypes.Remove(propertyTypes);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropertyTypeExists(int id)
        {
            return _dataContext.PropertyTypes.Any(e => e.Id == id);
        }
    }
}
