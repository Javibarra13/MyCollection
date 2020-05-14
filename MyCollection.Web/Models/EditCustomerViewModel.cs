using Microsoft.AspNetCore.Mvc.Rendering;
using MyCollection.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCollection.Web.Models
{
    public class EditCustomerViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Document")]
        [MaxLength(20, ErrorMessage = "The {0} field can not have more than {1} characters.")]
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

        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Address { get; set; }

        [Display(Name = "Phone Number")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string PhoneNumber { get; set; }

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
        public string RefName { get; set; }

        [Display(Name = "Reference Address")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string RefAddress { get; set; }

        [Display(Name = "Reference Phone")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string RefPhone { get; set; }

        [Display(Name = "Reference Name")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string RefName2 { get; set; }

        [Display(Name = "Reference Address")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string RefAddress2 { get; set; }

        [Display(Name = "Reference Phone")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string RefPhone2 { get; set; }

        public string Status { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "House")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a property type.")]
        public int HouseId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Collector")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a property type.")]
        public int CollectorId { get; set; }

        public IEnumerable<SelectListItem> Houses { get; set; }

        public IEnumerable<SelectListItem> Collectors { get; set; }
    }
}
