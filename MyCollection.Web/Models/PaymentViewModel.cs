using MyCollection.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCollection.Web.Models
{
    public class PaymentViewModel : Payment
    {
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Collector")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a collector.")]
        public int CollectorId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Customer")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a customer.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Concept")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a concept.")]
        public int ConceptId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Sale")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a sale.")]
        public int SaleId { get; set; }
    }
}
