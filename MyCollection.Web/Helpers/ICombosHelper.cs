using Microsoft.AspNetCore.Mvc.Rendering;
using MyCollection.Web.Data.Entities;
using System.Collections.Generic;

namespace MyCollection.Web.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboPropertyTypes();
        IEnumerable<SelectListItem> GetComboHouses();
        IEnumerable<SelectListItem> GetComboCollectors();
        IEnumerable<SelectListItem> GetComboLines();
        IEnumerable<SelectListItem> GetComboSublines();
        IEnumerable<SelectListItem> GetComboProviders();
        IEnumerable<SelectListItem> GetComboTypePayments();
        IEnumerable<SelectListItem> GetComboDayPayments();
        IEnumerable<SelectListItem> GetComboSellers();
        IEnumerable<SelectListItem> GetComboCustomers();
        IEnumerable<SelectListItem> GetComboStates();
        IEnumerable<SelectListItem> GetComboProducts();
        IEnumerable<SelectListItem> GetComboWarehouses();
        IEnumerable<SelectListItem> GetComboHelpers();
    }
}