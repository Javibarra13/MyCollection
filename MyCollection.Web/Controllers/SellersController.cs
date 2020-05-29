using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCollection.Web.Data;
using MyCollection.Web.Data.Entities;
using MyCollection.Web.Helpers;
using MyCollection.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCollection.Web.Controllers
{
    [Authorize(Roles = "Manager")]
    public class SellersController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IImageHelper _imageHelper;

        public SellersController(
            DataContext dataContext,
            IUserHelper userHelper,
            ICombosHelper combosHelper,
            IConverterHelper converterHelper,
            IImageHelper imageHelper)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
            _imageHelper = imageHelper;
        }

        public IActionResult Index()
        {
            return View(_dataContext.Sellers
                .Include(c => c.User)
                .Include(c => c.PropertySellers));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seller = await _dataContext.Sellers
                .Include(c => c.User)
                .Include(c => c.PropertySellers)
                .ThenInclude(pc => pc.PropertyType)
                .Include(c => c.PropertySellers)
                .ThenInclude(pc => pc.PropertySellerImages)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddUserViewModel view)
        {
            if (ModelState.IsValid)
            {
                var user = await AddUser(view);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "This email is already used.");
                    return View(view);
                }

                var seller = new Seller
                {
                    PropertySellers = new List<PropertySeller>(),
                    User = user,
                };

                _dataContext.Sellers.Add(seller);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(view);
        }

        private async Task<User> AddUser(AddUserViewModel view)
        {
            var user = new User
            {
                Address = view.Address,
                Document = view.Document,
                Email = view.Username,
                FirstName = view.FirstName,
                LastName = view.LastName,
                PhoneNumber = view.PhoneNumber,
                UserName = view.Username
            };

            var result = await _userHelper.AddUserAsync(user, view.Password);
            if (result != IdentityResult.Success)
            {
                return null;
            }

            var newUser = await _userHelper.GetUserByEmailAsync(view.Username);
            await _userHelper.AddUserToRoleAsync(newUser, "Seller");
            return newUser;
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seller = await _dataContext.Sellers
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == id.Value);
            if (seller == null)
            {
                return NotFound();
            }

            var model = new EditUserViewModel
            {
                Address = seller.User.Address,
                Document = seller.User.Document,
                FirstName = seller.User.FirstName,
                Id = seller.Id,
                LastName = seller.User.LastName,
                PhoneNumber = seller.User.PhoneNumber
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var seller = await _dataContext.Sellers
                    .Include(c => c.User)
                    .FirstOrDefaultAsync(o => o.Id == viewModel.Id);

                seller.User.Document = viewModel.Document;
                seller.User.FirstName = viewModel.FirstName;
                seller.User.LastName = viewModel.LastName;
                seller.User.Address = viewModel.Address;
                seller.User.PhoneNumber = viewModel.PhoneNumber;

                await _userHelper.UpdateUserAsync(seller.User);
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seller = await _dataContext.Sellers
                .Include(c => c.User)
                .Include(c => c.PropertySellers)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seller == null)
            {
                return NotFound();
            }

            if (seller.PropertySellers.Count != 0)
            {
                ModelState.AddModelError(string.Empty, "Seller has related registers");
                return RedirectToAction(nameof(Index));
            }

            _dataContext.Sellers.Remove(seller);
            await _dataContext.SaveChangesAsync();
            await _userHelper.DeleteUserAsync(seller.User.Email);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddProperty(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seller = await _dataContext.Sellers.FindAsync(id);
            if (seller == null)
            {
                return NotFound();
            }
            var model = new PropertySellerViewModel
            {
                SellerId = seller.Id,
                PropertyTypes = _combosHelper.GetComboPropertyTypes()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProperty(PropertySellerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var propertySeller = await _converterHelper.ToPropertySellerAsync(viewModel, true);
                _dataContext.PropertySellers.Add(propertySeller);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction("Details", "Sellers", new { id = viewModel.SellerId });
            }

            viewModel.PropertyTypes = _combosHelper.GetComboPropertyTypes();

            return View(viewModel);
        }

        public async Task<IActionResult> EditProperty(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertySeller = await _dataContext.PropertySellers
                .Include(pc => pc.Seller)
                .Include(pc => pc.PropertyType)
                .FirstOrDefaultAsync(pc => pc.Id == id);
            if (propertySeller == null)
            {
                return NotFound();
            }

            var model = _converterHelper.ToPropertySellerViewModel(propertySeller);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProperty(PropertySellerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var propertySeller = await _converterHelper.ToPropertySellerAsync(viewModel, false);
                _dataContext.PropertySellers.Update(propertySeller);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction("Details", "Sellers", new { id = viewModel.SellerId });
            }

            return View(viewModel);
        }

        public async Task<IActionResult> DetailsProperty(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertySeller = await _dataContext.PropertySellers
                .Include(o => o.Seller)
                .ThenInclude(o => o.User)
                .Include(o => o.PropertyType)
                .Include(p => p.PropertySellerImages)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (propertySeller == null)
            {
                return NotFound();
            }

            return View(propertySeller);
        }

        public async Task<IActionResult> DeleteProperty(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertySeller = await _dataContext.PropertySellers
                .Include(pc => pc.Seller)
                .Include(pc => pc.PropertySellerImages)
                .FirstOrDefaultAsync(pc => pc.Id == id.Value);
            if (propertySeller == null)
            {
                return NotFound();
            }

            _dataContext.PropertySellerImages.RemoveRange(propertySeller.PropertySellerImages);
            _dataContext.PropertySellers.Remove(propertySeller);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("Details", "Sellers", new { id = propertySeller.Seller.Id });
        }

        public async Task<IActionResult> AddImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertySeller = await _dataContext.PropertySellers.FindAsync(id.Value);
            if (propertySeller == null)
            {
                return NotFound();
            }

            var model = new PropertySellerImageViewModel
            {
                Id = propertySeller.Id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddImage(PropertySellerImageViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if (viewModel.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(viewModel.ImageFile);
                }

                var propertySellerImage = new PropertySellerImage
                {
                    ImageUrl = path,
                    PropertySeller = await _dataContext.PropertySellers.FindAsync(viewModel.Id)
                };

                _dataContext.PropertySellerImages.Add(propertySellerImage);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction("DetailsProperty", "Sellers", new { id = viewModel.Id });
            }

            return View(viewModel);
        }

        public async Task<IActionResult> DeleteImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertySellerImage = await _dataContext.PropertySellerImages
                .Include(pci => pci.PropertySeller)
                .FirstOrDefaultAsync(pci => pci.Id == id.Value);
            if (propertySellerImage == null)
            {
                return NotFound();
            }

            _dataContext.PropertySellerImages.Remove(propertySellerImage);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("DetailsProperty", "Sellers", new { id = propertySellerImage.PropertySeller.Id });
        }

        private bool SellerExists(int id)
        {
            return _dataContext.Sellers.Any(e => e.Id == id);
        }
    }
}
