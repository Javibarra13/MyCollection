using System;
using System.Collections.Generic;
using System.Text;

namespace MyCollection.Common.Models
{
    public class HelperResponse
    {
        public int Id { get; set; }

        public string Document { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Neighborhood { get; set; }

        public string City { get; set; }

        public decimal Bond { get; set; }

        public ICollection<OrderResponse> Orders { get; set; }

        public ICollection<SaleResponse> Sales { get; set; }
    }
}
