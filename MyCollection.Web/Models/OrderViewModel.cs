using Microsoft.AspNetCore.Mvc.Rendering;
using MyCollection.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCollection.Web.Models
{
    public class OrderViewModel : Order
    {
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "House")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a house.")]
        public int HouseId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Collector")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a collector.")]
        public int CollectorId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Type Payment")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a type payment.")]
        public int TypePaymentId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Day Payment")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a day payment.")]
        public int DayPaymentId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Seller")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a seller.")]
        public int SellerId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Customer")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a customer.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "State")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a state.")]
        public int StateId { get; set; }

        public List<OrderTmp> Details2 { get; set; }

        public List<OrderDetailTmp> Details { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public double TotalQuantity { get { return Details == null ? 0 : Details.Sum(d => d.Quantity); } }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public decimal TotalValue { get { return Details == null ? 0 : Details.Sum(d => d.Value); } }

        public IEnumerable<SelectListItem> Houses { get; set; }

        public IEnumerable<SelectListItem> TypePayments { get; set; }

        public IEnumerable<SelectListItem> DayPayments { get; set; }

        public IEnumerable<SelectListItem> Sellers { get; set; }

        //public IEnumerable<SelectListItem> Customers { get; set; }

        public IEnumerable<SelectListItem> States { get; set; }

        //public IEnumerable<SelectListItem> Collectors { get; set; }
    }
}
