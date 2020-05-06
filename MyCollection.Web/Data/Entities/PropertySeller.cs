using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCollection.Web.Data.Entities
{
    public class PropertySeller
    {
        public int Id { get; set; }

        [Display(Name = "Serie")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Serie { get; set; }

        [Display(Name = "Company")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Company { get; set; }

        [Display(Name = "Model")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Model { get; set; }

        [Display(Name = "Colour")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Colour { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }

        [Display(Name = "Is Available?")]
        public bool IsAvailable { get; set; }

        public string Remarks { get; set; }

        public PropertyType PropertyType { get; set; }

        public Seller Seller { get; set; }

        public ICollection<PropertySellerImage> PropertySellerImages { get; set; }
    }
}
