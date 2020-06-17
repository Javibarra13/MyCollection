using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyCollection.Web.Data.Entities
{
    public class Sale
    {
        public int Id { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime StartDateLocal => StartDate.ToLocalTime();

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime EndDateLocal => EndDate.ToLocalTime();

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public double Payment { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public double Deposit { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        public ICollection<Payment> Payments { get; set; }

        public Warehouse Warehouse { get; set; }

        public House House { get; set; }

        public Collector Collector { get; set; }

        [Display(Name = "Type Payment")]
        public TypePayment TypePayment { get; set; }

        [Display(Name = "Day Payment")]
        public DayPayment DayPayment { get; set; }

        public Seller Seller { get; set; }

        public Customer Customer { get; set; }

        public State State { get; set; }

        public ICollection<SaleDetail> SaleDetails { get; set; }
    }

    public static class Extensions
    {
        public static Expression<Func<Sale, bool>> IsPaid()
        {
            return s => s.Payments.Sum(p => p.Deposit) >= s.SaleDetails.Sum(sd => (double)sd.Price * sd.Quantity);
        }
        public static Expression<Func<Sale, bool>> IsntPaid()
        {
            return s => s.Payments.Sum(p => p.Deposit) < s.SaleDetails.Sum(sd => (double)sd.Price * sd.Quantity);
        }
    }
}
