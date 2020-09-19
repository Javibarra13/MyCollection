using System;
using System.Collections.Generic;
using System.Text;

namespace MyCollection.Common.Models
{
    public class PurchaseDetailResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public double Quantity { get; set; }

        public PurchaseResponse Purchase { get; set; }

        public ProductResponse Product { get; set; }
    }
}
