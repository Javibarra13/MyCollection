using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCollection.Web.Data.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Document { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string LastName { get; set; }

        [Display(Name = "Cell Phone")]
        [MaxLength(20, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string CellPhone { get; set; }

        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Address { get; set; }

        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Neighborhood { get; set; }

        [Display(Name = "Postal Code")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string PostalCode { get; set; }

        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string City { get; set; }

        public string Remarks { get; set; }

        [Display(Name = "Reference Name")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string RefName { get; set; }

        [Display(Name = "Reference Address")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string RefAddress { get; set; }

        [Display(Name = "Reference Phone")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string RefPhone { get; set; }

        [Display(Name = "Reference Name")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string RefName2 { get; set; }

        [Display(Name = "Reference Address")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string RefAddress2 { get; set; }

        [Display(Name = "Reference Phone")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string RefPhone2 { get; set; }

        public string Status { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";

        public ICollection<CustomerImage> CustomerImages { get; set; }
    }
}
