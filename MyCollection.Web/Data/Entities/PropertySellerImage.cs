using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCollection.Web.Data.Entities
{
    public class PropertySellerImage
    {
        public int Id { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        public PropertySeller PropertySeller { get; set; }

        // TODO: Change the path when publish
        public string ImageFullPath => string.IsNullOrEmpty(ImageUrl)
            ? "https://webstudio-mycollection.azurewebsites.net/images/PropertySellerImages/noImage.png"
            : $"https://webstudio-mycollection.azurewebsites.net{ImageUrl.Substring(1)}";
    }
}
