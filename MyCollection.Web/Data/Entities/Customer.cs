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

        [Display(Name = "Documento")]
        [MaxLength(20, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Document { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        [Display(Name = "Dirección")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Address { get; set; }

        [Display(Name = "Teléfono")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Colonia")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Neighborhood { get; set; }

        [Display(Name = "Codigo Postal")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string PostalCode { get; set; }

        [Display(Name = "Ciudad")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string City { get; set; }

        [Display(Name = "Notas")]
        public string Remarks { get; set; }

        [Display(Name = "Nombre Referencia 1")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string RefName { get; set; }

        [Display(Name = "Dirección Referencia 1")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string RefAddress { get; set; }

        [Display(Name = "Teléfono Referencia 1")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string RefPhone { get; set; }

        [Display(Name = "Nombre Referencia 2")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string RefName2 { get; set; }

        [Display(Name = "Dirección Referencia 2")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string RefAddress2 { get; set; }

        [Display(Name = "Teléfono Referencia 2")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string RefPhone2 { get; set; }

        [Display(Name = "Estado")]
        public string Status { get; set; }

        public House House { get; set; }

        public Collector Collector { get; set; }

        public ICollection<CustomerImage> CustomerImages { get; set; }

        public ICollection<Order> Orders { get; set; }

        public ICollection<OrderTmp> OrderTmps { get; set; }

        public ICollection<Sale> Sales { get; set; }

        public ICollection<SaleTmp> SaleTmps { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}
