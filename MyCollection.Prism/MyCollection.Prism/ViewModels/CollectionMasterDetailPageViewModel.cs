using MyCollection.Common.Models;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyCollection.Prism.ViewModels
{
    public class CollectionMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public CollectionMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            LoadMenus();
        }

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        private void LoadMenus()
        {
            var menus = new List<Menu>
            {
                new Menu
                {
                    Icon = "ic_home",
                    PageName = "CustomersPage",
                    Title = "Clientes"
                },

                new Menu
                {
                    Icon = "ic_list_alt2",
                    PageName = "SalesPage",
                    Title = "Ventas"
                },

                new Menu
                {
                    Icon = "ic_person",
                    PageName = "ModifyUserPage",
                    Title = "Modificar Usuario"
                },

                new Menu
                {
                    Icon = "ic_map",
                    PageName = "MapPage",
                    Title = "Mapa"
                },

                new Menu
                {
                    Icon = "ic_exit_to_app",
                    PageName = "LoginPage",
                    Title = "Cerrar Sesión"
                }
            };

            Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel(_navigationService)
                {
                    Icon = m.Icon,
                    PageName = m.PageName,
                    Title = m.Title
                }).ToList());
        }
    }
}
