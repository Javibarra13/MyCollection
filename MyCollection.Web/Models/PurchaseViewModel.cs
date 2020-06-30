using Microsoft.AspNetCore.Mvc.Rendering;
using MyCollection.Web.Data.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MyCollection.Web.Models
{
    public class PurchaseViewModel : Purchase
    {
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Almacen")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a warehouse.")]
        public int WarehouseId { get; set; }

        public List<PurchaseDetailTmp> Details { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public double TotalQuantity { get { return Details == null ? 0 : Details.Sum(d => d.Quantity); } }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public decimal TotalValue { get { return Details == null ? 0 : Details.Sum(d => d.Value); } }

        public IEnumerable<SelectListItem> Warehouses { get; set; }
    }
}
