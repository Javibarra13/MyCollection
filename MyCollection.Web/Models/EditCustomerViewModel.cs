using Microsoft.AspNetCore.Mvc.Rendering;
using MyCollection.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCollection.Web.Models
{
    public class EditCustomerViewModel : Customer
    {
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Casa Comercial")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a property type.")]
        public int HouseId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Cobrador")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a property type.")]
        public int CollectorId { get; set; }

        public IEnumerable<SelectListItem> Houses { get; set; }

        public IEnumerable<SelectListItem> Collectors { get; set; }
    }
}
