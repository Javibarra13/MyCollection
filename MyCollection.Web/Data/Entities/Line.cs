using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCollection.Web.Data.Entities
{
    public class Line
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        public ICollection<Subline> Sublines { get; set; }
    }
}
