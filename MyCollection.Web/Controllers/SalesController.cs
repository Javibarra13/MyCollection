using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCollection.Web.Data;
using MyCollection.Web.Data.Entities;
using MyCollection.Web.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MyCollection.Web.Controllers
{
    public class SalesController : Controller
    {
        private readonly DataContext _dataContext;

        public SalesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            return View(_dataContext.Sales
                .Include(s => s.Collector)
                .ThenInclude(c => c.User)
                .Include(s => s.Customer)
                .ThenInclude(c => c.User)
                .Include(s => s.State)
                .ToList());
        }

        // GET: Sales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _dataContext.Sales
                .Include(s => s.House)
                .Include(s => s.Collector)
                .ThenInclude(c => c.User)
                .Include(s => s.TypePayment)
                .Include(s => s.DayPayment)
                .Include(s => s.Seller)
                .ThenInclude(s => s.User)
                .Include(s => s.Customer)
                .ThenInclude(c => c.User)
                .Include(s => s.State)
                .Include(s => s.SaleDetails)
                .ThenInclude(sd => sd.Product)
                .ThenInclude(p => p.ProductImages)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // GET: Sales/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartDate,EndDate,Payment,Deposit,Remarks")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                _dataContext.Add(sale);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sale);
        }

        // GET: Sales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _dataContext.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }
            return View(sale);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartDate,EndDate,Payment,Deposit,Remarks")] Sale sale)
        {
            if (id != sale.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dataContext.Update(sale);
                    await _dataContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleExists(sale.Id))
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
            return View(sale);
        }

        public IActionResult AddOrder()
        {
            return View(_dataContext.Orders
                .Include(o => o.Collector)
                .ThenInclude(c => c.User)
                .Include(o => o.Customer)
                .ThenInclude(c => c.User)
                .Include(o => o.State)
                .Where(o => o.State.Id == 2)
                .ToList());
        }

        public async Task<IActionResult> AddOrders(int? id, AddCustomerViewModel viewModel)
        {
            var orderTmp = _dataContext.OrderTmps.Where(ot => ot.Username == User.Identity.Name).FirstOrDefault();

            var customer = await _dataContext.Customers.FindAsync(id);
            if (orderTmp == null)
            {
                orderTmp = new AddCustomerViewModel
                {
                    CustomerId = customer.Id,
                    Customer = await _dataContext.Customers.FindAsync(viewModel.Id),
                    Username = User.Identity.Name
                };

                _dataContext.OrderTmps.Add(orderTmp);
            }
            else
            {
                orderTmp.Customer = await _dataContext.Customers.FindAsync(viewModel.Id);
                _dataContext.Entry(orderTmp).State = EntityState.Modified;
            }
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("Create");
        }

        public IActionResult AddCustomer()
        {
            return View(_dataContext.Customers
                .Include(c => c.User)
                .Include(c => c.House)
                .Include(c => c.Collector)
                .ThenInclude(c => c.User));
        }

        public async Task<IActionResult> AddCustomers(int? id, AddCustomerSaleViewModel viewModel)
        {
            var saleTmp = _dataContext.SaleTmps.Where(ot => ot.Username == User.Identity.Name).FirstOrDefault();

            var customer = await _dataContext.Customers.FindAsync(id);
            if (saleTmp == null)
            {
                saleTmp = new AddCustomerSaleViewModel
                {
                    CustomerId = customer.Id,
                    Customer = await _dataContext.Customers.FindAsync(viewModel.Id),
                    Username = User.Identity.Name
                };

                _dataContext.SaleTmps.Add(saleTmp);
            }
            else
            {
                saleTmp.Customer = await _dataContext.Customers.FindAsync(viewModel.Id);
                _dataContext.Entry(saleTmp).State = EntityState.Modified;
            }
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("Create");
        }

        private bool SaleExists(int id)
        {
            return _dataContext.Sales.Any(e => e.Id == id);
        }
    }
}
