using System.ComponentModel.DataAnnotations;

namespace MyCollection.Common.Models
{
    public class EmailRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
