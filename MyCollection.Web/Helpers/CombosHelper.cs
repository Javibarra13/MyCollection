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
