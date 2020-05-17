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
    public class SublinesController : Controller
    {
        private readonly DataContext _context;

        public SublinesController(DataContext context)
        {
            _context = context;
        }

        // GET: Sublines
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sublines.ToListAsync());
        }

        // GET: Sublines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subline = await _context.Sublines
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subline == null)
            {
                return NotFound();
            }

            return View(subline);
        }

        // GET: Sublines/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sublines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Subline subline)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subline);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subline);
        }

        // GET: Sublines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subline = await _context.Sublines.FindAsync(id);
            if (subline == null)
            {
                return NotFound();
            }
            return View(subline);
        }

        // POST: Sublines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Subline subline)
        {
            if (id != subline.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subline);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SublineExists(subline.Id))
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
            return View(subline);
        }

        // GET: Sublines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subline = await _context.Sublines
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subline == null)
            {
                return NotFound();
            }

            return View(subline);
        }

        // POST: Sublines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subline = await _context.Sublines.FindAsync(id);
            _context.Sublines.Remove(subline);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SublineExists(int id)
        {
            return _context.Sublines.Any(e => e.Id == id);
        }
    }
}
