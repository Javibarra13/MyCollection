using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCollection.Web.Data;
using MyCollection.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace MyCollection.Web.Controllers
{
    public class ConceptsController : Controller
    {
        private readonly DataContext _dataContext;

        public ConceptsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            return View(_dataContext.Concepts.ToList());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concept = await _dataContext.Concepts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (concept == null)
            {
                return NotFound();
            }

            return View(concept);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Concept concept)
        {
            if (ModelState.IsValid)
            {
                _dataContext.Add(concept);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(concept);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concept = await _dataContext.Concepts.FindAsync(id);
            if (concept == null)
            {
                return NotFound();
            }
            return View(concept);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Concept concept)
        {
            if (id != concept.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dataContext.Update(concept);
                    await _dataContext.SaveChangesAsync();
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

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concept = await _dataContext.Concepts.FindAsync(id);

            if (concept == null)
            {
                return NotFound();
            }

            _dataContext.Concepts.Remove(concept);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConceptExists(int id)
        {
            return _dataContext.Concepts.Any(e => e.Id == id);
        }
    }
}
