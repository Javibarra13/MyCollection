using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCollection.Web.Data;
using MyCollection.Web.Data.Entities;
using MyCollection.Web.Helpers;
using MyCollection.Web.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace MyCollection.Web.Controllers
{
    public class SalesController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ICombosHelper _combosHelper;

        public SalesController(
            DataContext dataContext,
            ICombosHelper combosHelper)
        {
            _dataContext = dataContext;
            _combosHelper = combosHelper;
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

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _dataContext.Sales
                .Include(s => s.House)
                .Include(s => s.Warehouse)
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
        public IActionResult CreateFromOrder(int? id)
        {
            var order = _dataContext.Orders
                .Include(o => o.House)
                .Include(o => o.Warehouse)
                .Include(o => o.Collector)
                .ThenInclude(c => c.User)
                .Include(o => o.TypePayment)
                .Include(o => o.DayPayment)
                .Include(o => o.Seller)
                .ThenInclude(s => s.User)
                .Include(o => o.Customer)
                .ThenInclude(c => c.User)
                .Include(o => o.State)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefault(o => o.Id == id);

            var model = new SaleFromOrderViewModel
            {
                Houses = _combosHelper.GetComboHouses(),
                HouseId = order.House.Id,
                TypePayments = _combosHelper.GetComboTypePayments(),
                TypePaymentId = order.TypePayment.Id,
                DayPayments = _combosHelper.GetComboDayPayments(),
                DayPaymentId = order.DayPayment.Id,
                Sellers = _combosHelper.GetComboSellers(),
                SellerId = order.Seller.Id,
                Warehouses = _combosHelper.GetComboWarehouses(),
                WarehouseId = order.Warehouse.Id,
                CustomerId = order.Customer.Id,
                CollectorId = order.Collector.Id,
                StateId = 1,
                StartDate = order.StartDate.Date,
                EndDate = order.EndDate.Date,
                Deposit = order.Deposit,
                Payment = order.Payment,
                Remarks = order.Remarks,
                Details2 = _dataContext.Orders.Include(o => o.Customer).ThenInclude(c => c.User).Where(o => o.Id == id).ToList(),
                Details = _dataContext.OrderDetails.Include(od => od.Product).Where(sd => sd.Order.Id == id).ToList(),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFromOrder(int? id,SaleFromOrderViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                using (var transaction = new CommittableTransaction(new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    var user = await _dataContext.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefaultAsync();
                    try
                    {
                        var sale = new Sale
                        {
                            StartDate = viewModel.StartDate.ToUniversalTime(),
                            EndDate = viewModel.EndDate.ToUniversalTime(),
                            Payment = viewModel.Payment,
                            Deposit = viewModel.Deposit,
                            Remarks = viewModel.Remarks,
                            Customer = await _dataContext.Customers.FindAsync(viewModel.CustomerId),
                            Warehouse = await _dataContext.Warehouses.FindAsync(viewModel.WarehouseId),
                            House = await _dataContext.Houses.FindAsync(viewModel.HouseId),
                            DayPayment = await _dataContext.DayPayments.FindAsync(viewModel.DayPaymentId),
                            TypePayment = await _dataContext.TypePayments.FindAsync(viewModel.TypePaymentId),
                            Seller = await _dataContext.Sellers.FindAsync(viewModel.SellerId),
                            Collector = await _dataContext.Collectors.FindAsync(viewModel.CollectorId),
                            State = await _dataContext.States.FindAsync(viewModel.StateId),
                        };

                        _dataContext.Sales.Add(sale);
                        await _dataContext.SaveChangesAsync();

                        var payment = new Payment
                        {
                            Sale = await _dataContext.Sales.FindAsync(sale.Id),
                            Customer = await _dataContext.Customers.FindAsync(sale.Customer.Id),
                            Date = sale.StartDate,
                            Deposit = sale.Deposit,
                            Type = "Efectivo",
                            Collector = await _dataContext.Collectors.FindAsync(sale.Customer.Collector.Id),
                            Concept = await _dataContext.Concepts.FindAsync(8),
                        };

                        _dataContext.Payments.Add(payment);
                        await _dataContext.SaveChangesAsync();

                        var order = await _dataContext.Orders.FindAsync(id);
                        order.State = await _dataContext.States.FindAsync(viewModel.StateId);
                        _dataContext.Entry(order).State = EntityState.Modified;

                        var details = _dataContext.OrderDetails.Include(od => od.Product).Where(sd => sd.Order.Id == id).ToList();

                        foreach (var detail in details)
                        {
                            var saleDetail = new SaleDetail
                            {
                                Name = detail.Name,
                                Price = detail.Price,
                                Quantity = detail.Quantity,
                                Sale = await _dataContext.Sales.FindAsync(sale.Id),
                                Product = await _dataContext.Products.FindAsync(detail.Product.Id),
                            };
                            _dataContext.SaleDetails.Add(saleDetail);

                            var inventory = _dataContext.Inventories.Where(i => i.Product.Id == saleDetail.Product.Id).FirstOrDefault();
                                inventory.Stock -= (decimal)saleDetail.Quantity;
                                _dataContext.Entry(inventory).State = EntityState.Modified;
                        }
                        await _dataContext.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            viewModel.Houses = _combosHelper.GetComboHouses();
            viewModel.TypePayments = _combosHelper.GetComboTypePayments();
            viewModel.DayPayments = _combosHelper.GetComboDayPayments();
            viewModel.Sellers = _combosHelper.GetComboSellers();
            viewModel.Warehouses = _combosHelper.GetComboWarehouses();
            viewModel.Details2 = _dataContext.Orders.Include(o => o.Customer).ThenInclude(c => c.User).Where(o => o.Id == id).ToList();
            viewModel.Details = _dataContext.OrderDetails.Include(od => od.Product).Where(sd => sd.Order.Id == id).ToList();
            return View(viewModel);
        }

        public IActionResult Create()
        {
            var saleTmp = _dataContext.SaleTmps.Include(st => st.Customer).ThenInclude(c => c.Collector).Where(ot => ot.Username == User.Identity.Name).FirstOrDefault();
            var saleDetailTmp = _dataContext.SaleDetailTmps.Include(sdt => sdt.Product).ThenInclude(p => p.ProductImages).FirstOrDefault();

            var model = new SaleViewModel
            {
                Houses = _combosHelper.GetComboHouses(),
                TypePayments = _combosHelper.GetComboTypePayments(),
                DayPayments = _combosHelper.GetComboDayPayments(),
                Sellers = _combosHelper.GetComboSellers(),
                Warehouses = _combosHelper.GetComboWarehouses(),
                CustomerId = saleTmp.Customer.Id,
                CollectorId = saleTmp.Customer.Collector.Id,
                StateId = 1,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddYears(1),
                Details = _dataContext.SaleDetailTmps.Where(sdt => sdt.Username == User.Identity.Name).ToList(),
                Details2 = _dataContext.SaleTmps.Include(st => st.Customer).ThenInclude(c => c.User).Where(st => st.Username == User.Identity.Name).ToList(),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = new CommittableTransaction(new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    var user = await _dataContext.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefaultAsync();
                    try
                    {
                        var sale = new Sale
                        {
                            Id = viewModel.Id,
                            StartDate = viewModel.StartDate.ToUniversalTime(),
                            EndDate = viewModel.EndDate.ToUniversalTime(),
                            Payment = viewModel.Payment,
                            Deposit = viewModel.Deposit,
                            Remarks = viewModel.Remarks,
                            Customer = await _dataContext.Customers.FindAsync(viewModel.CustomerId),
                            Warehouse = await _dataContext.Warehouses.FindAsync(viewModel.WarehouseId),
                            House = await _dataContext.Houses.FindAsync(viewModel.HouseId),
                            DayPayment = await _dataContext.DayPayments.FindAsync(viewModel.DayPaymentId),
                            TypePayment = await _dataContext.TypePayments.FindAsync(viewModel.TypePaymentId),
                            Seller = await _dataContext.Sellers.FindAsync(viewModel.SellerId),
                            Collector = await _dataContext.Collectors.FindAsync(viewModel.CollectorId),
                            State = await _dataContext.States.FindAsync(viewModel.StateId),
                        };

                        var details2 = _dataContext.SaleTmps.Where(pt => pt.Username == User.Identity.Name).ToList();
                        foreach (var detail2 in details2)
                        {
                            sale = new Sale
                            {
                                Id = viewModel.Id,
                                StartDate = viewModel.StartDate.ToUniversalTime(),
                                EndDate = viewModel.EndDate.ToUniversalTime(),
                                Payment = viewModel.Payment,
                                Deposit = viewModel.Deposit,
                                Remarks = viewModel.Remarks,
                                Customer = await _dataContext.Customers.FindAsync(detail2.Customer.Id),
                                Warehouse = await _dataContext.Warehouses.FindAsync(viewModel.WarehouseId),
                                House = await _dataContext.Houses.FindAsync(viewModel.HouseId),
                                DayPayment = await _dataContext.DayPayments.FindAsync(viewModel.DayPaymentId),
                                TypePayment = await _dataContext.TypePayments.FindAsync(viewModel.TypePaymentId),
                                Seller = await _dataContext.Sellers.FindAsync(viewModel.SellerId),
                                Collector = await _dataContext.Collectors.FindAsync(detail2.Customer.Collector.Id),
                                State = await _dataContext.States.FindAsync(viewModel.StateId),
                            };

                            _dataContext.Sales.Add(sale);
                            _dataContext.SaleTmps.Remove(detail2);
                        }

                        var payment = new Payment
                        {
                            Sale = await _dataContext.Sales.FindAsync(sale.Id),
                            Customer = await _dataContext.Customers.FindAsync(sale.Customer.Id),
                            Date = sale.StartDate,
                            Deposit = sale.Deposit,
                            Type = "Efectivo",
                            Collector = await _dataContext.Collectors.FindAsync(sale.Customer.Collector.Id),
                            Concept = await _dataContext.Concepts.FindAsync(8),
                        };

                        _dataContext.Payments.Add(payment);
                        await _dataContext.SaveChangesAsync();

                        _dataContext.Sales.Add(sale);
                        await _dataContext.SaveChangesAsync();

                        var details = _dataContext.SaleDetailTmps.Include(sdt => sdt.Product).Where(sdt => sdt.Username == User.Identity.Name).ToList();

                        foreach (var detail in details)
                        {
                            var saleDetail = new SaleDetail
                            {
                                Name = detail.Name,
                                Price = detail.Price,
                                Quantity = detail.Quantity,
                                Sale = await _dataContext.Sales.FindAsync(sale.Id),
                                Product = await _dataContext.Products.FindAsync(detail.Product.Id),
                            };

                            _dataContext.SaleDetails.Add(saleDetail);
                            _dataContext.SaleDetailTmps.Remove(detail);

                            var inventory = _dataContext.Inventories.Where(i => i.Product.Id == saleDetail.Product.Id).FirstOrDefault();
                            inventory.Stock -= (decimal)saleDetail.Quantity;
                            _dataContext.Entry(inventory).State = EntityState.Modified;
                        }
                        await _dataContext.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            viewModel.Houses = _combosHelper.GetComboHouses();
            viewModel.TypePayments = _combosHelper.GetComboTypePayments();
            viewModel.DayPayments = _combosHelper.GetComboDayPayments();
            viewModel.Sellers = _combosHelper.GetComboSellers();
            viewModel.States = _combosHelper.GetComboStates();
            viewModel.Warehouses = _combosHelper.GetComboWarehouses();
            viewModel.Details = _dataContext.SaleDetailTmps.Where(sdt => sdt.Username == User.Identity.Name).ToList();
            viewModel.Details2 = _dataContext.SaleTmps.Include(st => st.Customer).ThenInclude(c => c.User).Where(ot => ot.Username == User.Identity.Name).ToList();
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int? id)
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
                .Include(s => s.Customer)
                .ThenInclude(c => c.Orders)
                .Include(s => s.State)
                .Include(s => s.SaleDetails)
                .ThenInclude(sd => sd.Product)
                .ThenInclude(p => p.ProductImages)
                .Include(s => s.Payments)
                .FirstOrDefaultAsync(s => s.Id == id);

            var saleDetail = await _dataContext.SaleDetails.Where(sd => sd.Sale.Id == id).FirstOrDefaultAsync();

            if (sale == null)
            {
                return NotFound();
            }

            var inventory = _dataContext.Inventories.Where(i => i.Product.Id == saleDetail.Product.Id).FirstOrDefault();
            inventory.Stock += (decimal)saleDetail.Quantity;
            _dataContext.Entry(inventory).State = EntityState.Modified;

            _dataContext.SaleDetails.RemoveRange(sale.SaleDetails);
            _dataContext.Payments.RemoveRange(sale.Payments);
            _dataContext.Sales.Remove(sale);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult AddProduct()
        {
            var model = new SaleDetailTmpViewModel
            {
                Products = _combosHelper.GetComboProducts()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(SaleDetailTmpViewModel view)
        {
            var user = await _dataContext.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefaultAsync();

            if (ModelState.IsValid)
            {
                var saleDetailTmp = _dataContext.SaleDetailTmps.Where(sdt => sdt.Username == User.Identity.Name && sdt.Product.Id == view.ProductId).FirstOrDefault();
                if (saleDetailTmp == null)
                {
                    var product = await _dataContext.Products.FindAsync(view.ProductId);
                    saleDetailTmp = new SaleDetailTmp
                    {
                        Id = view.Id,
                        Name = product.Name,
                        Price = view.Price,
                        Quantity = view.Quantity,
                        Username = User.Identity.Name,
                        Product = await _dataContext.Products.FindAsync(view.ProductId)
                    };
                    _dataContext.SaleDetailTmps.Add(saleDetailTmp);
                }
                else
                {
                    saleDetailTmp.Quantity += view.Quantity;
                    _dataContext.Entry(saleDetailTmp).State = EntityState.Modified;
                }
                await _dataContext.SaveChangesAsync();
                return RedirectToAction("Create");
            }

            view.Products = _combosHelper.GetComboProducts();
            return View(view);
        }

        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleDetailTmp = await _dataContext.SaleDetailTmps
                .FirstOrDefaultAsync(sdt => sdt.Id == id.Value);
            if (saleDetailTmp == null)
            {
                return NotFound();
            }

            _dataContext.SaleDetailTmps.Remove(saleDetailTmp);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("Create", "Sales");
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
