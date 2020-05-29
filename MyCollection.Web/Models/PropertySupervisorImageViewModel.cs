using Microsoft.AspNetCore.Http;
using MyCollection.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace MyCollection.Web.Models
{
    public class PropertySupervisorImageViewModel : PropertySupervisorImage
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}
