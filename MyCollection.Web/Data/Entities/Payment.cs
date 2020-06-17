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

        public Collector Collector { get; set; }

        public Sale Sale { get; set; }

        public Customer Customer { get; set; }

        public Concept Concept { get; set; }

        public string Type { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime Date { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime DateLocal => Date.ToLocalTime();

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public double Deposit { get; set; }
    }
}
