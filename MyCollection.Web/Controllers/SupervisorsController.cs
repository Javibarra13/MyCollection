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
    public class SupervisorsController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IImageHelper _imageHelper;

        public SupervisorsController(
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
            return View(_dataContext.Supervisors
                .Include(c => c.User)
                .Include(c => c.PropertySupervisors));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supervisor = await _dataContext.Supervisors
                .Include(c => c.User)
                .Include(c => c.PropertySupervisors)
                .ThenInclude(pc => pc.PropertyType)
                .Include(c => c.PropertySupervisors)
                .ThenInclude(pc => pc.PropertySupervisorImages)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supervisor == null)
            {
                return NotFound();
            }

            return View(supervisor);
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

                var supervisor = new Supervisor
                {
                    PropertySupervisors = new List<PropertySupervisor>(),
                    User = user,
                };

                _dataContext.Supervisors.Add(supervisor);
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
            await _userHelper.AddUserToRoleAsync(newUser, "Supervisor");
            return newUser;
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supervisor = await _dataContext.Supervisors
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == id.Value);
            if (supervisor == null)
            {
                return NotFound();
            }

            var model = new EditUserViewModel
            {
                Address = supervisor.User.Address,
                Document = supervisor.User.Document,
                FirstName = supervisor.User.FirstName,
                Id = supervisor.Id,
                LastName = supervisor.User.LastName,
                PhoneNumber = supervisor.User.PhoneNumber
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var supervisor = await _dataContext.Supervisors
                    .Include(c => c.User)
                    .FirstOrDefaultAsync(o => o.Id == viewModel.Id);

                supervisor.User.Document = viewModel.Document;
                supervisor.User.FirstName = viewModel.FirstName;
                supervisor.User.LastName = viewModel.LastName;
                supervisor.User.Address = viewModel.Address;
                supervisor.User.PhoneNumber = viewModel.PhoneNumber;

                await _userHelper.UpdateUserAsync(supervisor.User);
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

            var supervisor = await _dataContext.Supervisors
                .Include(c => c.User)
                .Include(c => c.PropertySupervisors)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supervisor == null)
            {
                return NotFound();
            }

            if (supervisor.PropertySupervisors.Count != 0)
            {
                ModelState.AddModelError(string.Empty, "Supervisor has related registers");
                return RedirectToAction(nameof(Index));
            }

            _dataContext.Supervisors.Remove(supervisor);
            await _dataContext.SaveChangesAsync();
            await _userHelper.DeleteUserAsync(supervisor.User.Email);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddProperty(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supervisor = await _dataContext.Supervisors.FindAsync(id);
            if (supervisor == null)
            {
                return NotFound();
            }
            var model = new PropertySupervisorViewModel
            {
                SupervisorId = supervisor.Id,
                PropertyTypes = _combosHelper.GetComboPropertyTypes()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProperty(PropertySupervisorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var propertySupervisor = await _converterHelper.ToPropertySupervisorAsync(viewModel, true);
                _dataContext.PropertySupervisors.Add(propertySupervisor);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction("Details", "Supervisors", new { id = viewModel.SupervisorId });
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

            var propertySupervisor = await _dataContext.PropertySupervisors
                .Include(pc => pc.Supervisor)
                .Include(pc => pc.PropertyType)
                .FirstOrDefaultAsync(pc => pc.Id == id);
            if (propertySupervisor == null)
            {
                return NotFound();
            }

            var model = _converterHelper.ToPropertySupervisorViewModel(propertySupervisor);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProperty(PropertySupervisorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var propertySupervisor = await _converterHelper.ToPropertySupervisorAsync(viewModel, false);
                _dataContext.PropertySupervisors.Update(propertySupervisor);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction("Details", "Supervisors", new { id = viewModel.SupervisorId });
            }

            return View(viewModel);
        }

        public async Task<IActionResult> DetailsProperty(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertySupervisor = await _dataContext.PropertySupervisors
                .Include(o => o.Supervisor)
                .ThenInclude(o => o.User)
                .Include(o => o.PropertyType)
                .Include(p => p.PropertySupervisorImages)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (propertySupervisor == null)
            {
                return NotFound();
            }

            return View(propertySupervisor);
        }

        public async Task<IActionResult> DeleteProperty(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertySupervisor = await _dataContext.PropertySupervisors
                .Include(pc => pc.Supervisor)
                .Include(pc => pc.PropertySupervisorImages)
                .FirstOrDefaultAsync(pc => pc.Id == id.Value);
            if (propertySupervisor == null)
            {
                return NotFound();
            }

            _dataContext.PropertySupervisorImages.RemoveRange(propertySupervisor.PropertySupervisorImages);
            _dataContext.PropertySupervisors.Remove(propertySupervisor);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("Details", "Supervisors", new { id = propertySupervisor.Supervisor.Id });
        }

        public async Task<IActionResult> AddImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertySupervisor = await _dataContext.PropertySupervisors.FindAsync(id.Value);
            if (propertySupervisor == null)
            {
                return NotFound();
            }

            var model = new PropertySupervisorImageViewModel
            {
                Id = propertySupervisor.Id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddImage(PropertySupervisorImageViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if (viewModel.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(viewModel.ImageFile);
                }

                var propertySupervisorImage = new PropertySupervisorImage
                {
                    ImageUrl = path,
                    PropertySupervisor = await _dataContext.PropertySupervisors.FindAsync(viewModel.Id)
                };

                _dataContext.PropertySupervisorImages.Add(propertySupervisorImage);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction("DetailsProperty", "Supervisors", new { id = viewModel.Id });
            }

            return View(viewModel);
        }

        public async Task<IActionResult> DeleteImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertySupervisorImage = await _dataContext.PropertySupervisorImages
                .Include(pci => pci.PropertySupervisor)
                .FirstOrDefaultAsync(pci => pci.Id == id.Value);
            if (propertySupervisorImage == null)
            {
                return NotFound();
            }

            _dataContext.PropertySupervisorImages.Remove(propertySupervisorImage);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("DetailsProperty", "Supervisors", new { id = propertySupervisorImage.PropertySupervisor.Id });
        }

        private bool SupervisorExists(int id)
        {
            return _dataContext.Supervisors.Any(e => e.Id == id);
        }
    }
}
