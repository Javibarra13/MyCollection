using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCollection.Web.Data.Entities
{
    public class Payment
    {
        public int Id { get; set; }

        [Display(Name = "Cobrador")]
        public Collector Collector { get; set; }

        [Display(Name = "Venta")]
        public Sale Sale { get; set; }

        [Display(Name = "Cliente")]
        public Customer Customer { get; set; }

        [Display(Name = "Concepto")]
        public Concept Concept { get; set; }

        [Display(Name = "Tipo")]
        public string Type { get; set; }

        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime Date { get; set; }

        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime DateLocal => Date.ToLocalTime();

        [Display(Name = "Abono")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public double Deposit { get; set; }


        [DisplayFormat(DataFormatString = "{0:N6}")]
        public double Latitude { get; set; }

        [DisplayFormat(DataFormatString = "{0:N6}")]
        public double Longitude { get; set; }
    }
}
