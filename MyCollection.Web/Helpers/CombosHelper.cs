using Microsoft.AspNetCore.Mvc.Rendering;
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
        public IEnumerable<SelectListItem> GetComboProducts()
        {
            var list = _dataContext.Products.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = $"{c.Id}"
            }).OrderBy(c => c.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select a product...)",
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
                Text = "(Select a state...)",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboCustomers()
        {
            var list = _dataContext.Customers.Select(c => new SelectListItem
            {
                Text = c.User.FirstName + " " + c.User.LastName,
                Value = $"{c.Id}"
            }).OrderBy(c => c.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select a customer...)",
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
                Text = "(Select a seller...)",
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
                Text = "(Select a day payment...)",
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
                Text = "(Select a type payment...)",
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
                Text = "(Select a provider...)",
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
                Text = "(Select a subline...)",
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
                Text = "(Select a line...)",
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
                Text = "(Select a collector...)",
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
                Text = "(Select a house...)",
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
                Text = "(Select a property type...)",
                Value = "0"
            });

            return list;
        }
    }
}
