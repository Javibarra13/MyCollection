using Microsoft.AspNetCore.Mvc.Rendering;
using MyCollection.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCollection.Web.Models
{
    public class PropertyManagerViewModel : PropertyManager
    {
        public int ManagerId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Tipo de Propiedad")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un tipo de propiedad.")]
        public int PropertyTypeId { get; set; }

        public IEnumerable<SelectListItem> PropertyTypes { get; set; }
    }
}
