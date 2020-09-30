using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCollection.Common.Models
{
    public class PropertyCollectorResponse
    {
        public int Id { get; set; }

        public string Serie { get; set; }

        public string Company { get; set; }

        public string Model { get; set; }

        public string Colour { get; set; }

        public decimal Price { get; set; }

        public bool IsAvailable { get; set; }

        public string Remarks { get; set; }

        public string PropertyType { get; set; }

        public ICollection<PropertyCollectorImageResponse> PropertyCollectorImages { get; set; }

        public string FirstImage => PropertyCollectorImages == null || PropertyCollectorImages.Count == 0 
            ? "https://webstudiomx.azurewebsites.net/images/PropertyCollectorImages/noImage.png"
            : PropertyCollectorImages.FirstOrDefault().ImageUrl;
        }
    }
