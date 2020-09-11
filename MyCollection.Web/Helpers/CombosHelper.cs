using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCollection.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCollection.Web.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _dataContext;
        public CombosHelper(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public IEnumerable<SelectListItem> GetComboWarehouses()
        {
            var list = _dataContext.Warehouses.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = $"{c.Id}"
            }).OrderBy(c => c.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Seleccione un almacen...)",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboHelpers()
        {
            var list = _dataContext.Helpers.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = $"{c.Id}"
            }).OrderBy(c => c.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Seleccione un ayudante...)",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboProducts()
        {
            var list = _dataContext.Products.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = $"{c.Id}"
            }).OrderBy(c => c.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Seleccione un producto...)",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboStates()
        {
            var list = _dataContext.States.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = $"{c.Id}"
            }).OrderBy(c => c.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Seleccione un estado...)",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboCustomers()
        {
            var list = _dataContext.Customers.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = $"{c.Id}"
            }).OrderBy(c => c.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Seleccione un cliente...)",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboSellers()
        {
            var list = _dataContext.Sellers.Select(c => new SelectListItem
            {
                Text = c.User.FirstName + " " + c.User.LastName,
                Value = $"{c.Id}"
            }).OrderBy(c => c.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Seleccione un vendedor...)",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboDayPayments()
        {
            var list = _dataContext.DayPayments.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = $"{c.Id}"
            }).OrderBy(c => c.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Seleccione el dia de pago...)",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboTypePayments()
        {
            var list = _dataContext.TypePayments.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = $"{c.Id}"
            }).OrderBy(c => c.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Seleccione el tipo de pago...)",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboProviders()
        {
            var list = _dataContext.Providers.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = $"{c.Id}"
            }).OrderBy(c => c.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Seleccione un proveedor...)",
                Value = "0"
            });

            return list;
        }
        public IEnumerable<SelectListItem> GetComboSublines()
        {
            var list = _dataContext.Sublines.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = $"{c.Id}"
            }).OrderBy(c => c.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Seleccione una sublinea...)",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboLines()
        {
            var list = _dataContext.Lines.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = $"{c.Id}"
            }).OrderBy(c => c.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Seleccione una linea...)",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboCollectors()
        {
            var list = _dataContext.Collectors.Select(c => new SelectListItem
            {
                Text = c.User.FirstName + " " + c.User.LastName,
                Value = $"{c.Id}"
            }).OrderBy(c => c.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Seleccione un cobrador...)",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboHouses()
        {
            var list = _dataContext.Houses.Select(pt => new SelectListItem
            {
                Text = pt.Name,
                Value = $"{pt.Id}"
            }).OrderBy(pt => pt.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Seleccione una casa comercial...)",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboPropertyTypes()
        {
            var list = _dataContext.PropertyTypes.Select(pt => new SelectListItem
            {
                Text = pt.Name,
                Value = $"{pt.Id}"
            }).OrderBy(pt => pt.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Seleccione un tipo de propiedad...)",
                Value = "0"
            });

            return list;
        }
    }
}
