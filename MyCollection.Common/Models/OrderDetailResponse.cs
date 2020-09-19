using System;
using System.Collections.Generic;
using System.Text;

namespace MyCollection.Common.Models
{
    public class OrderDetailResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public double Quantity { get; set; }

        public OrderResponse Order { get; set; }

        public ProductResponse Product { get; set; }

        public decimal Value { get { return Price * (decimal)Quantity; } }
    }
}
