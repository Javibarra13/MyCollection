using Microsoft.AspNetCore.Mvc.Rendering;
using MyCollection.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCollection.Web.Models
{
    public class SaleDetailTmpViewModel : SaleDetailTmp
    {
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Producto")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un producto.")]
        public int ProductId { get; set; }

        public IEnumerable<SelectListItem> Products { get; set; }
    }
}
