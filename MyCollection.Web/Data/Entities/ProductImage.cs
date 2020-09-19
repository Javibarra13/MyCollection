using System.ComponentModel.DataAnnotations;

namespace MyCollection.Web.Data.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        public Product Product { get; set; }

        public string ImageFullPath => string.IsNullOrEmpty(ImageUrl)
    ? "https://mycollectionweb.azurewebsites.net/images/ProductImages/noImage.png"
    : $"https://mycollectionweb.azurewebsites.net{ImageUrl.Substring(1)}";
    }
}
