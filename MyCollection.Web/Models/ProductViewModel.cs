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
        [Display(Name = "Line")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a line.")]
        public int LineId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Subline")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a subline.")]
        public int SublineId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Provider")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a provider.")]
        public int ProviderId { get; set; }

        public IEnumerable<SelectListItem> Lines { get; set; }

        public IEnumerable<SelectListItem> Sublines { get; set; }

        public IEnumerable<SelectListItem> Providers { get; set; }
    }
}
