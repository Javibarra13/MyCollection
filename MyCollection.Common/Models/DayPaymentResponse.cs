using System;
using System.Collections.Generic;
using System.Text;

namespace MyCollection.Common.Models
{
    public class DayPaymentResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<OrderResponse> Orders { get; set; }

        public ICollection<SaleResponse> Sales { get; set; }
    }
}
