using System;
using System.Collections.Generic;
using System.Text;

namespace MyCollection.Common.Models
{
    public class SaleDetailResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public double Quantity { get; set; }
    }
}
