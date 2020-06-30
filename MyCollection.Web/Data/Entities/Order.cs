using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCollection.Web.Data.Entities
{
    public class Order
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

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
