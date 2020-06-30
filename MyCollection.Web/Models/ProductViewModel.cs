using Microsoft.AspNetCore.Mvc.Rendering;
using MyCollection.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCollection.Web.Models
{
    public class ProductViewModel : Product
    {

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Linea")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una linea.")]
        public int LineId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Sublinea")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una sublinea.")]
        public int SublineId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Proveedor")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un proveedor.")]
        public int ProviderId { get; set; }

        public IEnumerable<SelectListItem> Lines { get; set; }

        public IEnumerable<SelectListItem> Sublines { get; set; }

        public IEnumerable<SelectListItem> Providers { get; set; }
    }
}
