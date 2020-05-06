using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCollection.Web.Data.Entities
{
    public class PropertyManagerImage
    {
        public int Id { get; set; }

        [Display(Name = "Image")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string ImageUrl { get; set; }

        public PropertyManager PropertyManager { get; set; }

        // TODO: Change the path when publish
        public string ImageFullPath => $"https://TBD.azurewebsites.net{ImageUrl.Substring(1)}";
    }
}
