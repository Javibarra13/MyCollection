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
    public class ConceptsController : Controller
    {
        private readonly DataContext _context;

        public ConceptsController(DataContext context)
        {
            _context = context;
        }

        // GET: Concepts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Concepts.ToListAsync());
        }

        // GET: Concepts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concept = await _context.Concepts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (concept == null)
            {
                return NotFound();
            }

            return View(concept);
        }

        // GET: Concepts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Concepts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Type,IsAvailable")] Concept concept)
        {
            if (ModelState.IsValid)
            {
                _context.Add(concept);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(concept);
        }

        // GET: Concepts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concept = await _context.Concepts.FindAsync(id);
            if (concept == null)
            {
                return NotFound();
            }
            return View(concept);
        }

        // POST: Concepts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Type,IsAvailable")] Concept concept)
        {
            if (id != concept.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(concept);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConceptExists(concept.Id))
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
            return View(concept);
        }

        // GET: Concepts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concept = await _context.Concepts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (concept == null)
            {
                return NotFound();
            }

            return View(concept);
        }

        // POST: Concepts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var concept = await _context.Concepts.FindAsync(id);
            _context.Concepts.Remove(concept);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConceptExists(int id)
        {
            return _context.Concepts.Any(e => e.Id == id);
        }
    }
}
