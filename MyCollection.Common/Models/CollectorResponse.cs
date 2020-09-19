using System;
using System.Collections.Generic;
using System.Text;

namespace MyCollection.Common.Models
{
    public class CollectorResponse
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Document { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public ICollection<PropertyCollectorResponse> PropertyCollectors { get; set; }

        public ICollection<CustomerResponse> Customers { get; set; }

        //public ICollection<OrderResponse> Orders { get; set; }

        public ICollection<SaleResponse> Sales { get; set; }

        //public ICollection<PaymentResponse> Payments { get; set; }

        public string FullName => $"{FirstName} {LastName}";

    }
}
