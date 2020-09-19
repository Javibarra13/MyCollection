using System;
using System.Collections.Generic;
using System.Text;

namespace MyCollection.Common.Models
{
    public class SellerResponse
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Document { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public decimal Bond { get; set; }

        public ICollection<PropertySellerResponse> PropertySellers { get; set; }

        public ICollection<OrderResponse> Orders { get; set; }

        public ICollection<SaleResponse> Sales { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
