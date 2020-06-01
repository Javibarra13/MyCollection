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
    public class ProvidersController : Controller
    {
        private readonly DataContext _dataContext;

        public ProvidersController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            return View(_dataContext.Providers.ToList());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provider = await _dataContext.Providers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (provider == null)
            {
                return NotFound();
            }

            return View(provider);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Provider provider)
        {
            if (ModelState.IsValid)
            {
                _dataContext.Add(provider);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(provider);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provider = await _dataContext.Providers.FindAsync(id);
            if (provider == null)
            {
                return NotFound();
            }
            return View(provider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Provider provider)
        {
            if (id != provider.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dataContext.Update(provider);
                    await _dataContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProviderExists(provider.Id))
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
            return View(provider);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provider = await _dataContext.Providers.FindAsync(id);
            if (provider == null)
            {
                return NotFound();
            }

            _dataContext.Providers.Remove(provider);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProviderExists(int id)
        {
            return _dataContext.Providers.Any(e => e.Id == id);
        }
    }
}
