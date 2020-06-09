using Microsoft.AspNetCore.Mvc.Rendering;
using MyCollection.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCollection.Web.Models
{
    public class OrderDetailTmpViewModel : OrderDetailTmp
    {
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Product")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a product.")]
        public int ProductId { get; set; }

        public IEnumerable<SelectListItem> Products { get; set; }
    }
}
