using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCollection.Common.Models
{
    public class CustomerResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Document { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string FullName => $"{Name}";

        public ICollection<CustomerImageResponse> CustomerImages { get; set; }

        public string FirstImage => CustomerImages == null || CustomerImages.Count == 0
            ? "https://webstudio-mycollection.azurewebsites.net/images/CustomerImages/noImage.png"
            : CustomerImages.FirstOrDefault().ImageUrl;
    }
}
