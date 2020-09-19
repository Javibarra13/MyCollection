using System;
using System.Collections.Generic;
using System.Text;

namespace MyCollection.Common.Models
{
    public class StateResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<OrderResponse> Orders { get; set; }

        public ICollection<SaleResponse> Sales { get; set; }
    }
}
