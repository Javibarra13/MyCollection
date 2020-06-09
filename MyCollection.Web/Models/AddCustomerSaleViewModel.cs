using Microsoft.AspNetCore.Mvc.Rendering;
using MyCollection.Web.Data.Entities;
using System.Collections.Generic;

namespace MyCollection.Web.Models
{
    public class AddCustomerSaleViewModel : SaleTmp
    {
        public int CustomerId { get; set; }

        public IEnumerable<SelectListItem> Customers { get; set; }
    }
}
