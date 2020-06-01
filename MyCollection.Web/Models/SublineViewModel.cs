using Microsoft.AspNetCore.Mvc.Rendering;
using MyCollection.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCollection.Web.Models
{
    public class SublineViewModel : Subline
    {
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Line")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a line...")]
        public int LineId { get; set; }

        public IEnumerable<SelectListItem> Lines { get; set; }
    }
}
