using System;
using System.Collections.Generic;
using System.Text;

namespace MyCollection.Common.Models
{
    public class ProductResponse
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Barcode { get; set; }

        public string Name { get; set; }

        public string PurchaseUnit { get; set; }

        public string Sale { get; set; }

        public string Factor { get; set; }

        public decimal IVA { get; set; }

        public string Location { get; set; }

        public string Remarks { get; set; }

        public decimal Price { get; set; }

        public decimal Price2 { get; set; }

        public decimal Price3 { get; set; }

        public decimal Price4 { get; set; }

        public decimal Price5 { get; set; }

        public decimal ReorderPoint { get; set; }

        public decimal LastCost { get; set; }

        public bool IsAvailable { get; set; }

        public LineResponse Line { get; set; }

        public SublineResponse Subline { get; set; }

        public ProviderResponse Provider { get; set; }

        public ICollection<InventoryResponse> Inventories { get; set; }

        public ICollection<PurchaseDetailResponse> PurchaseDetails { get; set; }

        public ICollection<ProductImageResponse> ProductImages { get; set; }

        public ICollection<OrderDetailResponse> OrderDetails { get; set; }

        public ICollection<OrderDetailTmpResponse> OrderDetailTmps { get; set; }

        public ICollection<SaleDetailResponse> SaleDetails { get; set; }

        public ICollection<SaleDetailTmpResponse> SaleDetailTmps { get; set; }
    }
}
