using Microsoft.AspNetCore.Mvc.Rendering;
using MyCollection.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCollection.Web.Models
{
    public class CustomerViewModel : Customer
    {
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Casa Comercial")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una casa comercial.")]
        public int HouseId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Cobrador")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un cobrador.")]
        public int CollectorId { get; set; }

        public IEnumerable<SelectListItem> Houses { get; set; }

        public IEnumerable<SelectListItem> Collectors { get; set; }
    }
}
