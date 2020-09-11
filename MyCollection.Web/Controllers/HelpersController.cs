using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCollection.Web.Data;
using MyCollection.Web.Data.Entities;
using Vereyon.Web;

namespace MyCollection.Web.Controllers
{
    public class HelpersController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFlashMessage _flashMessage;

        public HelpersController(
            DataContext dataContext,
            IFlashMessage flashMessage)
        {
            _dataContext = dataContext;
            _flashMessage = flashMessage;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.Helpers.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var helper = await _dataContext.Helpers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (helper == null)
            {
                return NotFound();
            }

            return View(helper);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Helper helper)
        {
            if (ModelState.IsValid)
            {
                _dataContext.Add(helper);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(helper);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var helper = await _dataContext.Helpers.FindAsync(id);
            if (helper == null)
            {
                return NotFound();
            }
            return View(helper);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Helper helper)
        {
            if (id != helper.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dataContext.Update(helper);
                    await _dataContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HelperExists(helper.Id))
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
            return View(helper);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var helper = await _dataContext.Helpers
                .Include(h => h.Sales)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (helper == null)
            {
                return NotFound();
            }

            if (helper.Sales.Count != 0)
            {
                _flashMessage.Danger("Registro no pudo ser eliminado, cuenta con registros relacionados.");
                return RedirectToAction(nameof(Index));
            }
            else
            {
                    _dataContext.Helpers.Remove(helper);
                    await _dataContext.SaveChangesAsync();
                    _flashMessage.Danger("Registro eliminado con éxito.");

                return RedirectToAction(nameof(Index));
            }
        } 

        private bool HelperExists(int id)
        {
            return _dataContext.Helpers.Any(e => e.Id == id);
        }
    }
}
