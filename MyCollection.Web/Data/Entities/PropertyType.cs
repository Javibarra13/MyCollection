using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCollection.Web.Data.Entities
{
    public class PropertyType
    {
        public int Id { get; set; }

        [Display(Name = "Property Type")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        public ICollection<PropertySeller> PropertySellers { get; set; }

        public ICollection<PropertyManager> PropertyManagers { get; set; }

        public ICollection<PropertySupervisor> PropertySupervisors { get; set; }

        public ICollection<PropertyCollector> PropertyCollectors { get; set; }
    }
}
