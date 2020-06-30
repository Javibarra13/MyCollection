using Microsoft.AspNetCore.Mvc.Rendering;
using MyCollection.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCollection.Web.Models
{
    public class PurchaseDetailTmpViewModel : PurchaseDetailTmp
    {
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Producto")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un producto.")]
        public int ProductId { get; set; }

        public IEnumerable<SelectListItem> Products { get; set; }
    }
}
