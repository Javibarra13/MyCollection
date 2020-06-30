using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCollection.Web.Data.Entities
{
    public class PropertySupervisor
    {
        public int Id { get; set; }

        [Display(Name = "Serie")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Serie { get; set; }

        [Display(Name = "Compañia")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Company { get; set; }

        [Display(Name = "Modelo")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Model { get; set; }

        [Display(Name = "Color")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Colour { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }

        [Display(Name = "Está disponible ?")]
        public bool IsAvailable { get; set; }

        [Display(Name = "Notas")]
        public string Remarks { get; set; }

        [Display(Name = "Tipo de Propiedad")]
        public PropertyType PropertyType { get; set; }

        public Supervisor Supervisor { get; set; }

        public ICollection<PropertySupervisorImage> PropertySupervisorImages { get; set; }
    }
}
