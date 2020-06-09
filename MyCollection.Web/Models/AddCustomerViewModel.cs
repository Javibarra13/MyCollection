using Microsoft.AspNetCore.Mvc.Rendering;
using MyCollection.Web.Data.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCollection.Web.Models
{
    public class AddCustomerViewModel : OrderTmp
    {
        public int CustomerId { get; set; }

        public IEnumerable<SelectListItem> Customers { get; set; }
    }
}
