using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCollection.Web.Data;
using MyCollection.Web.Data.Entities;
using MyCollection.Web.Helpers;
using MyCollection.Web.Models;

namespace MyCollection.Web.Controllers
{
    public class SublinesController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;

        public SublinesController(
            DataContext dataContext,
            ICombosHelper combosHelper,
            IConverterHelper converterHelper)
        {
            _dataContext = dataContext;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
        }

        public IActionResult Index()
        {
            return View(_dataContext.Sublines
                .Include(s => s.Line)
                .Include(s => s.Products)
                .ToList());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subline = await _dataContext.Sublines
                .Include(s => s.Line)
                .Include(s => s.Products)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subline == null)
            {
                return NotFound();
            }

            return View(subline);
        }

        public IActionResult Create()
        {
            var model = new SublineViewModel
            {
                Lines = _combosHelper.GetComboLines()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SublineViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var subline = await _converterHelper.ToSublineAsync(viewModel, true);
                _dataContext.Sublines.Add(subline);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subline = await _dataContext.Sublines
                .Include(s => s.Line)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (subline == null)
            {
                return NotFound();
            }

            var model = _converterHelper.ToSublineViewModel(subline);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SublineViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var subline = await _converterHelper.ToSublineAsync(viewModel, false);
                _dataContext.Sublines.Update(subline);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction("Details", "Sublines", new { id = viewModel.Id });
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subline = await _dataContext.Sublines
                .Include(s => s.Line)
                .Include(s => s.Products)
                .FirstOrDefaultAsync(pc => pc.Id == id.Value);
            if (subline == null)
            {
                return NotFound();
            }

            if (subline.Products.Count != 0)
            {
                ModelState.AddModelError(string.Empty, "Subline has related registers");
                return RedirectToAction(nameof(Index));
            }

            _dataContext.Sublines.Remove(subline);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SublineExists(int id)
        {
            return _dataContext.Sublines.Any(e => e.Id == id);
        }
    }
}
