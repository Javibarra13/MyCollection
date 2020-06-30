using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCollection.Web.Data;
using MyCollection.Web.Data.Entities;
using MyCollection.Web.Helpers;
using MyCollection.Web.Models;

namespace MyCollection.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IImageHelper _imageHelper;

        public ProductsController(
            DataContext dataContext,
            ICombosHelper combosHelper,
            IConverterHelper converterHelper,
            IImageHelper imageHelper)
        {
            _dataContext = dataContext;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
            _imageHelper = imageHelper;
        }

        public IActionResult Index()
        {
            return View(_dataContext.Products
                .Include(p => p.Line)
                .Include(p => p.Subline)
                .Include(p => p.Inventories)
                .Include(p => p.Provider));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _dataContext.Products
                .Include(p => p.Line)
                .Include(p => p.Inventories)
                .ThenInclude(i => i.Warehouse)
                .Include(p => p.Subline)
                .Include(p => p.Provider)
                .Include(p => p.ProductImages) 
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public IActionResult Create()
        {
            var model = new ProductViewModel
            {
                Lines = _combosHelper.GetComboLines(),
                Sublines = _combosHelper.GetComboSublines(),
                Providers = _combosHelper.GetComboProviders()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var product = await _converterHelper.ToProductAsync(viewModel, true);
                _dataContext.Products.Add(product);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            viewModel.Lines = _combosHelper.GetComboLines();
            viewModel.Sublines = _combosHelper.GetComboSublines();
            viewModel.Providers = _combosHelper.GetComboProviders();
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _dataContext.Products
            .Include(p => p.Line)
            .Include(p => p.Subline)
            .Include(p => p.Provider)
            .FirstOrDefaultAsync(s => s.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            var model = _converterHelper.ToProductViewModel(product);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var product = await _converterHelper.ToProductAsync(viewModel, false);
                _dataContext.Products.Update(product);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction("Details", "Products", new { id = viewModel.Id });
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _dataContext.Products
                .Include(p => p.Line)
                .Include(p => p.Subline)
                .Include(p => p.Provider)
                .Include(p => p.PurchaseDetails)
                .FirstOrDefaultAsync(pc => pc.Id == id.Value);

            if (product == null)
            {
                return NotFound();
            }

            if (product.PurchaseDetails.Count != 0)
            {
                ModelState.AddModelError(string.Empty, "Product has related registers");
                return RedirectToAction(nameof(Index));
            }

            _dataContext.Products.Remove(product);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _dataContext.Products.FindAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            var model = new ProductImageViewModel
            {
                Id = product.Id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddImage(ProductImageViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if (viewModel.ImageFile != null)
                {
                    path = await _imageHelper.UploadProductImageAsync(viewModel.ImageFile);
                }

                var productImage = new ProductImage
                {
                    ImageUrl = path,
                    Product = await _dataContext.Products.FindAsync(viewModel.Id)
                };

                _dataContext.ProductImages.Add(productImage);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction("Details", "Products", new { id = viewModel.Id });
            }

            return View(viewModel);
        }

        private bool ProductExists(int id)
        {
            return _dataContext.Products.Any(e => e.Id == id);
        }
    }
}
