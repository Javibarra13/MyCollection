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
    public class LinesController : Controller
    {
        private readonly DataContext _dataContext;

        public LinesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // GET: Lines
        public IActionResult Index()
        {
            return View(_dataContext.Lines
                .Include(l => l.Products)
                .Include(l => l.Sublines)
                .ToList());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var line = await _dataContext.Lines
                .Include(l => l.Products)
                .Include(l => l.Sublines)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (line == null)
            {
                return NotFound();
            }

            return View(line);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Line line)
        {
            if (ModelState.IsValid)
            {
                _dataContext.Add(line);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(line);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var line = await _dataContext.Lines.FindAsync(id);
            if (line == null)
            {
                return NotFound();
            }
            return View(line);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Line line)
        {
            if (id != line.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dataContext.Update(line);
                    await _dataContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LineExists(line.Id))
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
            return View(line);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var line = await _dataContext.Lines
                .Include(l => l.Products)
                .Include(l => l.Sublines)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (line == null)
            {
                return NotFound();
            }

            if (line.Sublines.Count != 0)
            {
                ModelState.AddModelError(string.Empty, "Line has related registers");
                return RedirectToAction(nameof(Index));
            }

            _dataContext.Lines.Remove(line);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LineExists(int id)
        {
            return _dataContext.Lines.Any(e => e.Id == id);
        }
    }
}
