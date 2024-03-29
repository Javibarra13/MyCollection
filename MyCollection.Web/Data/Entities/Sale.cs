﻿using System;
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

        [Display(Name = "Fecha Inicio")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime StartDate { get; set; }

        [Display(Name = "Fecha Final")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Fecha Inicio")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime StartDateLocal => StartDate.ToLocalTime();

        [Display(Name = "Fecha Final")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime EndDateLocal => EndDate.ToLocalTime();

        [Display(Name = "Abono")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public double Payment { get; set; }

        [Display(Name = "Enganche")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public double Deposit { get; set; }

        [Display(Name = "Notas")]
        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        public ICollection<Payment> Payments { get; set; }

        [Display(Name = "Almacen")]
        public Warehouse Warehouse { get; set; }

        [Display(Name = "Casa Comercial")]
        public House House { get; set; }

        [Display(Name = "Cobrador")]
        public Collector Collector { get; set; }

        [Display(Name = "Tipo de Pago")]
        public TypePayment TypePayment { get; set; }

        [Display(Name = "Dia de Pago")]
        public DayPayment DayPayment { get; set; }

        [Display(Name = "Vendedor")]
        public Seller Seller { get; set; }

        [Display(Name = "Cliente")]
        public Customer Customer { get; set; }

        [Display(Name = "Estado")]
        public State State { get; set; }

        [Display(Name = "Ayudante")]
        public Helper Helper { get; set; }

        [Display(Name = "Pagado?")]
        public bool Pending { get; set; }

        public ICollection<SaleDetail> SaleDetails { get; set; }
    }

    public static class Extension
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
