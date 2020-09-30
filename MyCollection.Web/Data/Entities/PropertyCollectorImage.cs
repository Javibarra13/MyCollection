using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCollection.Web.Data.Entities
{
    public class PropertyCollectorImage
    {
        public int Id { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        public PropertyCollector PropertyCollector { get; set; }

        // TODO: Change the path when publish
        public string ImageFullPath => string.IsNullOrEmpty(ImageUrl)
            ? "https://webstudiomx.azurewebsites.net/images/PropertyCollectorImages/noImage.png"
            : $"https://webstudiomx.azurewebsites.net{ImageUrl.Substring(1)}";

    }
}
