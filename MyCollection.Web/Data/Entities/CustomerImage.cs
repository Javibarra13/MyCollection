using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCollection.Web.Data.Entities
{
    public class CustomerImage
    {
        public int Id { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        public Customer Customer { get; set; }

        public string ImageFullPath => string.IsNullOrEmpty(ImageUrl)
    ? "https://webstudio-mycollection.azurewebsites.net/images/CustomerImages/noImage.png"
    : $"https://webstudio-mycollection.azurewebsites.net{ImageUrl.Substring(1)}";
    }
}
