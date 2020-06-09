using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCollection.Web.Data.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Neighborhood { get; set; }

        [Display(Name = "Postal Code")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string PostalCode { get; set; }

        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string City { get; set; }

        public string Remarks { get; set; }

        [Display(Name = "Reference Name")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string RefName { get; set; }

        [Display(Name = "Reference Address")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string RefAddress { get; set; }

        [Display(Name = "Reference Phone")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string RefPhone { get; set; }

        [Display(Name = "Reference Name")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string RefName2 { get; set; }

        [Display(Name = "Reference Address")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string RefAddress2 { get; set; }

        [Display(Name = "Reference Phone")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string RefPhone2 { get; set; }

        public string Status { get; set; }

        public User User { get; set; }

        public House House { get; set; }

        public Collector Collector { get; set; }

        public ICollection<CustomerImage> CustomerImages { get; set; }

        public ICollection<Purchase> Purchases { get; set; }

        public ICollection<Order> Orders { get; set; }

        public ICollection<OrderTmp> OrderTmps { get; set; }

        public ICollection<Sale> Sales { get; set; }

        public ICollection<SaleTmp> SaleTmps { get; set; }
    }
}
