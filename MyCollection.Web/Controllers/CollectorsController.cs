﻿using Microsoft.AspNetCore.Authorization;
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
using Vereyon.Web;

namespace MyCollection.Web.Controllers
{
    [Authorize(Roles = "Manager")]
    public class CollectorsController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IImageHelper _imageHelper;
        private readonly IFlashMessage _flashMessage;
        private readonly IMailHelper _mailHelper;

        public CollectorsController(
            DataContext dataContext,
            IUserHelper userHelper,
            ICombosHelper combosHelper,
            IConverterHelper converterHelper,
            IImageHelper imageHelper,
            IFlashMessage flashMessage,
            IMailHelper mailHelper)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
            _imageHelper = imageHelper;
            _flashMessage = flashMessage;
            _mailHelper = mailHelper;
        }

        public IActionResult Index()
        {
            return View(_dataContext.Collectors
                .Include(c => c.User)
                .Include(c => c.PropertyCollectors));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collector = await _dataContext.Collectors
                .Include(c => c.User)
                .Include(c => c.PropertyCollectors)
                .ThenInclude(pc => pc.PropertyType)
                .Include(c => c.PropertyCollectors)
                .ThenInclude(pc => pc.PropertyCollectorImages)
                .Include(c => c.Payments)
                .ThenInclude(p => p.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collector == null)
            {
                return NotFound();
            }

            return View(collector);
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
                    ModelState.AddModelError(string.Empty, "Email esta actualmente en uso.");
                    return View(view);
                }

                var collector = new Collector
                {
                    PropertyCollectors = new List<PropertyCollector>(),
                    User = user,
                };

                _dataContext.Collectors.Add(collector);
                await _dataContext.SaveChangesAsync();

                var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                var tokenLink = Url.Action("ConfirmEmail", "Account", new
                {
                    userid = user.Id,
                    token = myToken
                }, protocol: HttpContext.Request.Scheme);

                _mailHelper.SendMail(view.Username, "Email confirmation",
                $"<table style = 'max-width: 600px; padding: 10px; margin:0 auto; border-collapse: collapse;'>" +
                $"  <tr>" +
                $"    <td style = 'background-color: #34495e; text-align: center; padding: 0'>" +
                $"       <a href = 'https://www.facebook.com/NuskeCIV/' >" +
                $"         <img width = '20%' style = 'display:block; margin: 1.5% 3%' src= 'https://veterinarianuske.com/wp-content/uploads/2016/10/line_separator.png'>" +
                $"       </a>" +
                $"  </td>" +
                $"  </tr>" +
                $"  <tr>" +
                $"  <td style = 'padding: 0'>" +
                $"     <img style = 'padding: 0; display: block' src = 'https://mycollectionweb.azurewebsites.net/images/Logo_WS.jpg' width = '100%'>" +
                $"  </td>" +
                $"</tr>" +
                $"<tr>" +
                $" <td style = 'background-color: #ecf0f1'>" +
                $"      <div style = 'color: #34495e; margin: 4% 10% 2%; text-align: justify;font-family: sans-serif'>" +
                $"            <h1 style = 'color: #e67e22; margin: 0 0 7px' > Hola </h1>" +
                $"                    <p style = 'margin: 2px; font-size: 15px'>" +
                $"                      La mejor empresa para el desarrollo de sistemas y aplicaciones Web de Sonora enfocado a brindar servicios garantizados <br>" +
                $"                      aplicando las técnicas más actuales y equipo de vanguardia para proceder a realizar un trabajo preciso y efisciente..<br>" +
                $"                      Entre los servicios tenemos:</p>" +
                $"      <ul style = 'font-size: 15px;  margin: 10px 0'>" +
                $"        <li> Desarrollo Web.</li>" +
                $"        <li> Desarrollo Aplicaciones Android.</li>" +
                $"        <li> Desarrollo Aplicaciones IOS.</li>" +
                $"        <li> Marketing Web</li>" +
                $"      </ul>" +
                $"  <div style = 'width: 100%;margin:20px 0; display: inline-block;text-align: center'>" +
                $"    <img style = 'padding: 0; width: 200px; margin: 5px' src = 'https://veterinarianuske.com/wp-content/uploads/2018/07/tarjetas.png'>" +
                $"  </div>" +
                $"  <div style = 'width: 100%; text-align: center'>" +
                $"    <h2 style = 'color: #e67e22; margin: 0 0 7px' >Confirmación Email</h2>" +
                $"    Para habilitar este usuario presione el enlace:" +
                $"    <a style ='text-decoration: none; border-radius: 5px; padding: 11px 23px; color: white; background-color: #3498db' href = \"{tokenLink}\">Confirmar</a>" +
                $"    <p style = 'color: #b3b3b3; font-size: 12px; text-align: center;margin: 30px 0 0' > WebStudio MX 2020 </p>" +
                $"  </div>" +
                $" </td >" +
                $"</tr>" +
                $"</table>");

                _flashMessage.Confirmation("Registro creado con éxito.");
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
            await _userHelper.AddUserToRoleAsync(newUser, "Collector");
            return newUser;
        }

        public async Task<IActionResult> AddProperty(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collector = await _dataContext.Collectors.FindAsync(id);
            if (collector == null)
            {
                return NotFound();
            }
            var model = new PropertyCollectorViewModel
            {
                CollectorId = collector.Id,
                PropertyTypes = _combosHelper.GetComboPropertyTypes()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProperty(PropertyCollectorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var propertyCollector = await _converterHelper.ToPropertyCollectorAsync(viewModel, true);
                _dataContext.PropertyCollectors.Add(propertyCollector);
                await _dataContext.SaveChangesAsync();
                _flashMessage.Confirmation("Registro creado con éxito.");
                return RedirectToAction("Details","Collectors", new { id = viewModel.CollectorId });
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

            var propertyCollector = await _dataContext.PropertyCollectors
                .Include(pc => pc.Collector)
                .Include(pc => pc.PropertyType)
                .FirstOrDefaultAsync(pc => pc.Id == id);
            if (propertyCollector == null)
            {
                return NotFound();
            }

            var model = _converterHelper.ToPropertyCollectorViewModel(propertyCollector);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProperty(PropertyCollectorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var propertyCollector = await _converterHelper.ToPropertyCollectorAsync(viewModel, false);
                _dataContext.PropertyCollectors.Update(propertyCollector);
                await _dataContext.SaveChangesAsync();
                _flashMessage.Info("Registro editado con éxito.");
                return RedirectToAction("Details", "Collectors", new { id = viewModel.CollectorId });
            }

            return View(viewModel);
        }

        public async Task<IActionResult> DetailsProperty(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyCollector = await _dataContext.PropertyCollectors
                .Include(o => o.Collector)
                .ThenInclude(o => o.User)
                .Include(o => o.PropertyType)
                .Include(p => p.PropertyCollectorImages)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (propertyCollector == null)
            {
                return NotFound();
            }

            return View(propertyCollector);
        }

        public async Task<IActionResult> AddImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyCollector = await _dataContext.PropertyCollectors.FindAsync(id.Value);
            if (propertyCollector == null)
            {
                return NotFound();
            }

            var model = new PropertyCollectorImageViewModel
            {
                Id = propertyCollector.Id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddImage(PropertyCollectorImageViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if (viewModel.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(viewModel.ImageFile);
                }

                var propertyCollectorImage = new PropertyCollectorImage
                {
                    ImageUrl = path,
                    PropertyCollector = await _dataContext.PropertyCollectors.FindAsync(viewModel.Id)
                };

                _dataContext.PropertyCollectorImages.Add(propertyCollectorImage);
                await _dataContext.SaveChangesAsync();
                _flashMessage.Confirmation("Registro creado con éxito.");
                return RedirectToAction("DetailsProperty", "Collectors", new { id = viewModel.Id });
            }

            return View(viewModel);
        }

        public async Task<IActionResult> DeleteImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyCollectorImage = await _dataContext.PropertyCollectorImages
                .Include(pci => pci.PropertyCollector)
                .FirstOrDefaultAsync(pci => pci.Id == id.Value);
            if (propertyCollectorImage == null)
            {
                return NotFound();
            }

            _dataContext.PropertyCollectorImages.Remove(propertyCollectorImage);
            await _dataContext.SaveChangesAsync();
            _flashMessage.Danger("Registro eliminado con éxito.");
            return RedirectToAction("DetailsProperty", "Collectors", new { id = propertyCollectorImage.PropertyCollector.Id });
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collector = await _dataContext.Collectors
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == id.Value);
            if (collector == null)
            {
                return NotFound();
            }

            var model = new EditUserViewModel
            {
                Address = collector.User.Address,
                Document = collector.User.Document,
                FirstName = collector.User.FirstName,
                Id = collector.Id,
                LastName = collector.User.LastName,
                PhoneNumber = collector.User.PhoneNumber
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var collector = await _dataContext.Collectors
                    .Include(c => c.User)
                    .FirstOrDefaultAsync(o => o.Id == viewModel.Id);

                collector.User.Document = viewModel.Document;
                collector.User.FirstName = viewModel.FirstName;
                collector.User.LastName = viewModel.LastName;
                collector.User.Address = viewModel.Address;
                collector.User.PhoneNumber = viewModel.PhoneNumber;

                await _userHelper.UpdateUserAsync(collector.User);
                _flashMessage.Info("Registro editado con éxito.");
                return RedirectToAction("Details", "Collectors", new { id = viewModel.Id });
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collector = await _dataContext.Collectors
                .Include(c => c.User)
                .Include(c => c.PropertyCollectors)
                .Include(c => c.Customers)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collector == null)
            {
                return NotFound();
            }

            if (collector.PropertyCollectors.Count != 0 || collector.Customers.Count != 0)
            {
                _flashMessage.Danger("Registro no pudo ser eliminado, cuenta con registros relacionados.");
                return RedirectToAction(nameof(Index));
            }
            else 
            {
                try
                {
                    _dataContext.Collectors.Remove(collector);
                    await _dataContext.SaveChangesAsync();
                    await _userHelper.DeleteUserAsync(collector.User.Email);
                    _flashMessage.Danger("Registro eliminado con éxito.");
                }
                catch
                {
                    _flashMessage.Danger("Registro no pudo ser eliminado, cuenta con registros relacionados.");
                }
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> DeleteProperty(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyCollector = await _dataContext.PropertyCollectors
                .Include(pc => pc.Collector)
                .Include(pc => pc.PropertyCollectorImages)
                .FirstOrDefaultAsync(pc => pc.Id == id.Value);
            if (propertyCollector == null)
            {
                return NotFound();
            }

            _dataContext.PropertyCollectorImages.RemoveRange(propertyCollector.PropertyCollectorImages);
            _dataContext.PropertyCollectors.Remove(propertyCollector);
            await _dataContext.SaveChangesAsync();
            _flashMessage.Danger("Registro eliminado con éxito.");
            return RedirectToAction("Details", "Collectors", new { id = propertyCollector.Collector.Id });
        }



        private bool CollectorExists(int id)
        {
            return _dataContext.Collectors.Any(e => e.Id == id);
        }
    }
}
