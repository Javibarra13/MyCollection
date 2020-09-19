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
        public string ImageUrl { get; set; }

        public PropertyManager PropertyManager { get; set; }

        // TODO: Change the path when publish
        public string ImageFullPath => string.IsNullOrEmpty(ImageUrl)
            ? "https://mycollectionweb.azurewebsites.net/images/PropertyManagerImages/noImage.png"
            : $"https://mycollectionweb.azurewebsites.net{ImageUrl.Substring(1)}";
    }
}
