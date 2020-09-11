using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCollection.Web.Data.Entities
{
    public class Seller
    {
        public int Id { get; set; }

        public User User { get; set; }

        [Display(Name = "Comisión")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = false)]
        public decimal Bond { get; set; }

        public ICollection<PropertySeller> PropertySellers { get; set; }

        public ICollection<Order> Orders { get; set; }

        public ICollection<Sale> Sales { get; set; }
    }
}
