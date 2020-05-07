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
    public class CollectorsController : Controller
    {
        private readonly DataContext _context;

        public CollectorsController(DataContext context)
        {
            _context = context;
        }

        // GET: Collectors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Collectors.ToListAsync());
        }

        // GET: Collectors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collector = await _context.Collectors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collector == null)
            {
                return NotFound();
            }

            return View(collector);
        }

        // GET: Collectors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Collectors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] Collector collector)
        {
            if (ModelState.IsValid)
            {
                _context.Add(collector);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(collector);
        }

        // GET: Collectors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collector = await _context.Collectors.FindAsync(id);
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
                    _context.Update(collector);
                    await _context.SaveChangesAsync();
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

            var collector = await _context.Collectors
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
            var collector = await _context.Collectors.FindAsync(id);
            _context.Collectors.Remove(collector);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CollectorExists(int id)
        {
            return _context.Collectors.Any(e => e.Id == id);
        }
    }
}
