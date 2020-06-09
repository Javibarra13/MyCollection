using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Manager")]
    public class OrdersController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;

        public OrdersController(
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
            return View(_dataContext.Orders
                .Include(o => o.House)
                .Include(o => o.Collector)
                .ThenInclude(c => c.User)
                .Include(o => o.TypePayment)
                .Include(o => o.DayPayment)
                .Include(o => o.Seller)
                .ThenInclude(s => s.User)
                .Include(o => o.Customer)
                .ThenInclude(c => c.User)
                .Include(o => o.State)
                .ToList());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _dataContext.Orders
                .Include(o => o.House)
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
                .ThenInclude(p => p.ProductImages)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public IActionResult Create()
        {
            var orderTmp = _dataContext.OrderTmps.Include(ot => ot.Customer).ThenInclude(c => c.Collector).Where(ot => ot.Username == User.Identity.Name).FirstOrDefault();
            var orderDetailTmp = _dataContext.OrderDetailTmps.Include(odt => odt.Product).ThenInclude(p => p.ProductImages).FirstOrDefault();

                var model = new OrderViewModel
                {
                    Houses = _combosHelper.GetComboHouses(),
                    TypePayments = _combosHelper.GetComboTypePayments(),
                    DayPayments = _combosHelper.GetComboDayPayments(),
                    Sellers = _combosHelper.GetComboSellers(),
                    CustomerId = orderTmp.Customer.Id,
                    CollectorId = orderTmp.Customer.Collector.Id,
                    StateId = 2,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddYears(1),
                    Details = _dataContext.OrderDetailTmps.Where(odt => odt.Username == User.Identity.Name).ToList(),
                    Details2 = _dataContext.OrderTmps.Include(ot => ot.Customer).ThenInclude(c => c.User).Where(ot => ot.Username == User.Identity.Name).ToList(),
                };
             return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = new CommittableTransaction(new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    var user = await _dataContext.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefaultAsync();
                    try
                    {
                        var order = new Order
                        {
                            Id = viewModel.Id,
                            StartDate = viewModel.StartDate.ToUniversalTime(),
                            EndDate = viewModel.EndDate.ToUniversalTime(),
                            Payment = viewModel.Payment,
                            Deposit = viewModel.Deposit,
                            Remarks = viewModel.Remarks,
                            Customer = await _dataContext.Customers.FindAsync(viewModel.CustomerId),
                            House = await _dataContext.Houses.FindAsync(viewModel.HouseId),
                            DayPayment = await _dataContext.DayPayments.FindAsync(viewModel.DayPaymentId),
                            TypePayment = await _dataContext.TypePayments.FindAsync(viewModel.TypePaymentId),
                            Seller = await _dataContext.Sellers.FindAsync(viewModel.SellerId),
                            Collector = await _dataContext.Collectors.FindAsync(viewModel.CollectorId),
                            State = await _dataContext.States.FindAsync(viewModel.StateId),
                        };

                        var details2 = _dataContext.OrderTmps.Where(pt => pt.Username == User.Identity.Name).ToList();
                        foreach (var detail2 in details2)
                        {
                            order = new Order
                            {
                                Id = viewModel.Id,
                                StartDate = viewModel.StartDate.ToUniversalTime(),
                                EndDate = viewModel.EndDate.ToUniversalTime(),
                                Payment = viewModel.Payment,
                                Deposit = viewModel.Deposit,
                                Remarks = viewModel.Remarks,
                                Customer = await _dataContext.Customers.FindAsync(detail2.Customer.Id),
                                House = await _dataContext.Houses.FindAsync(viewModel.HouseId),
                                DayPayment = await _dataContext.DayPayments.FindAsync(viewModel.DayPaymentId),
                                TypePayment = await _dataContext.TypePayments.FindAsync(viewModel.TypePaymentId),
                                Seller = await _dataContext.Sellers.FindAsync(viewModel.SellerId),
                                Collector = await _dataContext.Collectors.FindAsync(detail2.Customer.Collector.Id),
                                State = await _dataContext.States.FindAsync(viewModel.StateId),
                            };

                            _dataContext.Orders.Add(order);
                            _dataContext.OrderTmps.Remove(detail2);
                        }

                        _dataContext.Orders.Add(order);
                        await _dataContext.SaveChangesAsync();

                        var details = _dataContext.OrderDetailTmps.Include(odt => odt.Product).Where(odt => odt.Username == User.Identity.Name).ToList();

                        foreach (var detail in details)
                        {
                            var orderDetail = new OrderDetail
                            {
                                Name = detail.Name,
                                Price = detail.Price,
                                Quantity = detail.Quantity,
                                Order = await _dataContext.Orders.FindAsync(order.Id),
                                Product = await _dataContext.Products.FindAsync(detail.Product.Id),
                            };

                            _dataContext.OrderDetails.Add(orderDetail);
                            _dataContext.OrderDetailTmps.Remove(detail);
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
            viewModel.Details = _dataContext.OrderDetailTmps.Where(odt => odt.Username == User.Identity.Name).ToList();
            viewModel.Details2 = _dataContext.OrderTmps.Include(ot => ot.Customer).ThenInclude(c => c.User).Where(ot => ot.Username == User.Identity.Name).ToList();
            return View(viewModel);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _dataContext.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartDate,EndDate,Payment,Deposit,Remarks")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dataContext.Update(order);
                    await _dataContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            return View(order);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _dataContext.Orders
                .Include(o => o.House)
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
                .ThenInclude(p => p.ProductImages)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            _dataContext.OrderDetails.RemoveRange(order.OrderDetails);
            _dataContext.Orders.Remove(order);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult AddProduct()
        {
            var model = new OrderDetailTmpViewModel
            {
                Products = _combosHelper.GetComboProducts()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(OrderDetailTmpViewModel view)
        {
            var user = await _dataContext.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefaultAsync();

            if (ModelState.IsValid)
            {
                var orderDetailTmp = _dataContext.OrderDetailTmps.Where(odt => odt.Username == User.Identity.Name && odt.Product.Id == view.ProductId).FirstOrDefault();
                if (orderDetailTmp == null)
                {
                    var product = await _dataContext.Products.FindAsync(view.ProductId);
                    orderDetailTmp = new OrderDetailTmp
                    {
                        Id = view.Id,
                        Name = product.Name,
                        Price = view.Price,
                        Quantity = view.Quantity,
                        Username = User.Identity.Name,
                        Product = await _dataContext.Products.FindAsync(view.ProductId)
                    };
                    _dataContext.OrderDetailTmps.Add(orderDetailTmp);
                }
                else
                {
                    orderDetailTmp.Quantity += view.Quantity;
                    _dataContext.Entry(orderDetailTmp).State = EntityState.Modified;
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

            var orderDetailTmp = await _dataContext.OrderDetailTmps
                .FirstOrDefaultAsync(pci => pci.Id == id.Value);
            if (orderDetailTmp == null)
            {
                return NotFound();
            }

            _dataContext.OrderDetailTmps.Remove(orderDetailTmp);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("Create", "Orders");
        }

        public IActionResult AddCustomer()
        {
            return View(_dataContext.Customers
                .Include(c => c.User)
                .Include(c => c.House)
                .Include(c => c.Collector)
                .ThenInclude(c => c.User));
        }

        public async Task<IActionResult> AddCustomers(int? id, AddCustomerViewModel viewModel)
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

        private bool OrderExists(int id)
        {
            return _dataContext.Orders.Any(e => e.Id == id);
        }
    }
}
