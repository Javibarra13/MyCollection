using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCollection.Web.Data;
using MyCollection.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace MyCollection.Web.Controllers
{
    [Authorize(Roles = "Manager")]
    public class HousesController : Controller
    {
        private readonly DataContext _dataContext;

        public HousesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            return View(_dataContext.Houses
                .Include(h => h.Customers)
                .ToList());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var house = await _dataContext.Houses
                .Include(h => h.Customers)
                .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (house == null)
            {
                return NotFound();
            }

            return View(house);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(House house)
        {
            if (ModelState.IsValid)
            {
                _dataContext.Add(house);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(house);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var house = await _dataContext.Houses.FindAsync(id);
            if (house == null)
            {
                return NotFound();
            }
            return View(house);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,House house)
        {
            if (id != house.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dataContext.Update(house);
                    await _dataContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HouseExists(house.Id))
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
            return View(house);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var house = await _dataContext.Houses
                .Include(l => l.Customers)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (house == null)
            {
                return NotFound();
            }

            if (house.Customers.Count != 0)
            {
                ModelState.AddModelError(string.Empty, "House has related registers");
                return RedirectToAction(nameof(Index));
            }

            _dataContext.Houses.Remove(house);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HouseExists(int id)
        {
            return _dataContext.Houses.Any(e => e.Id == id);
        }
    }
}
