using System;
using System.Collections.Generic;
using System.Text;

namespace MyCollection.Common.Models
{
    public class WarehouseResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Neighborhood { get; set; }

        public string City { get; set; }

        public string Phone { get; set; }

        public bool IsMain { get; set; }

        public bool IsAvailable { get; set; }

        public string Contact { get; set; }

        public ICollection<InventoryResponse> Inventories { get; set; }

        public ICollection<PurchaseResponse> Purchases { get; set; }

        public ICollection<OrderResponse> Orders { get; set; }

        public ICollection<SaleResponse> Sales { get; set; }
    }
}
