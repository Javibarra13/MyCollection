using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCollection.Web.Data;
using MyCollection.Web.Data.Entities;
using MyCollection.Web.Helpers;
using MyCollection.Web.Models;

namespace MyCollection.Web.Controllers
{
    public class PurchasesController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ICombosHelper _combosHelper;

        public PurchasesController(
            DataContext dataContext,
            ICombosHelper combosHelper)
        {
            _dataContext = dataContext;
            _combosHelper = combosHelper;
        }

        public IActionResult Index()
        {
            return View(_dataContext.Purchases
                .Include(p => p.Warehouse)
                .ToList());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _dataContext.Purchases
                .Include(p => p.Warehouse)
                .Include(p => p.PurchaseDetails)
                .ThenInclude(sd => sd.Product)
                .ThenInclude(p => p.ProductImages)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }


        public IActionResult Create()
        {
            var model = new PurchaseViewModel
            {
                StartDate = DateTime.Today,
                Warehouses = _combosHelper.GetComboWarehouses(),
                Details = _dataContext.PurchaseDetailTmps.Where(sdt => sdt.Username == User.Identity.Name).ToList(),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PurchaseViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = new CommittableTransaction(new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    var user = await _dataContext.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefaultAsync();
                    try
                    {
                        var purchase = new Purchase
                        {
                            Id = viewModel.Id,
                            StartDate = viewModel.StartDate.ToUniversalTime(),
                            Warehouse = await _dataContext.Warehouses.FindAsync(viewModel.WarehouseId),
                            Remarks = viewModel.Remarks
                        };

                        _dataContext.Purchases.Add(purchase);
                        await _dataContext.SaveChangesAsync();

                        var details = _dataContext.PurchaseDetailTmps.Include(pdt => pdt.Product).Where(pdt => pdt.Username == User.Identity.Name).ToList();

                        foreach (var detail in details)
                        {
                            var purchaseDetail = new PurchaseDetail
                            {
                                Name = detail.Name,
                                Price = detail.Price,
                                Quantity = detail.Quantity,
                                Purchase = await _dataContext.Purchases.FindAsync(purchase.Id),
                                Product = await _dataContext.Products.FindAsync(detail.Product.Id),
                            };
                            _dataContext.PurchaseDetails.Add(purchaseDetail);

                            var inventory = _dataContext.Inventories.Where(i => i.Product.Id == purchaseDetail.Product.Id && i.Warehouse.Id == purchase.Warehouse.Id).FirstOrDefault();
                            if (inventory == null)
                            {
                                inventory = new Inventory
                                {
                                    Product = await _dataContext.Products.FindAsync(detail.Product.Id),
                                    Warehouse = await _dataContext.Warehouses.FindAsync(viewModel.WarehouseId),
                                    Stock = (decimal)detail.Quantity,
                                };
                                _dataContext.Inventories.Add(inventory);
                            }
                            else
                            {
                                inventory.Stock += (decimal)purchaseDetail.Quantity;
                                _dataContext.Entry(inventory).State = EntityState.Modified;
                            }
                            _dataContext.PurchaseDetailTmps.Remove(detail);
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
            viewModel.Warehouses = _combosHelper.GetComboWarehouses();
            viewModel.Details = _dataContext.PurchaseDetailTmps.Where(pdt => pdt.Username == User.Identity.Name).ToList();
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _dataContext.Purchases
                .Include(p => p.Warehouse)
                .Include(p => p.PurchaseDetails)
                .ThenInclude(sd => sd.Product)
                .ThenInclude(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == id);

            var purchaseDetail = await _dataContext.PurchaseDetails.Where(pd => pd.Purchase.Id == id).FirstOrDefaultAsync();

            if (purchase == null)
            {
                return NotFound();
            }

            var inventory = _dataContext.Inventories.Where(i => i.Product.Id == purchaseDetail.Product.Id).FirstOrDefault();
            inventory.Stock -= (decimal)purchaseDetail.Quantity;
            _dataContext.Entry(inventory).State = EntityState.Modified;

            _dataContext.PurchaseDetails.RemoveRange(purchase.PurchaseDetails);
            _dataContext.Purchases.Remove(purchase);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult AddProduct()
        {
            var model = new PurchaseDetailTmpViewModel
            {
                Products = _combosHelper.GetComboProducts()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(PurchaseDetailTmpViewModel view)
        {
            var user = await _dataContext.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefaultAsync();

            if (ModelState.IsValid)
            {
                var purchaseDetailTmp = _dataContext.PurchaseDetailTmps.Where(sdt => sdt.Username == User.Identity.Name && sdt.Product.Id == view.ProductId).FirstOrDefault();
                if (purchaseDetailTmp == null)
                {
                    var product = await _dataContext.Products.FindAsync(view.ProductId);
                    purchaseDetailTmp = new PurchaseDetailTmp
                    {
                        Id = view.Id,
                        Name = product.Name,
                        Price = view.Price,
                        Quantity = view.Quantity,
                        Username = User.Identity.Name,
                        Product = await _dataContext.Products.FindAsync(view.ProductId)
                    };
                    _dataContext.PurchaseDetailTmps.Add(purchaseDetailTmp);
                }
                else
                {
                    purchaseDetailTmp.Quantity += view.Quantity;
                    _dataContext.Entry(purchaseDetailTmp).State = EntityState.Modified;
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

        private bool PurchaseExists(int id)
        {
            return _dataContext.Purchases.Any(e => e.Id == id);
        }
    }
}
