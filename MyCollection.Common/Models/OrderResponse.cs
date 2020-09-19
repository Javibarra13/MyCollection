using System;
using System.Collections.Generic;
using System.Text;

namespace MyCollection.Common.Models
{
    public class OrderResponse
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime StartDateLocal => StartDate.ToLocalTime();

        public DateTime EndDateLocal => EndDate.ToLocalTime();

        public double Payment { get; set; }

        public double Deposit { get; set; }

        public string Remarks { get; set; }

        public WarehouseResponse Warehouse { get; set; }

        public HouseResponse House { get; set; }

        public CollectorResponse Collector { get; set; }

        public TypePaymentResponse TypePayment { get; set; }

        public DayPaymentResponse DayPayment { get; set; }

        public SellerResponse Seller { get; set; }

        public CustomerResponse Customer { get; set; }

        public StateResponse State { get; set; }

        public HelperResponse Helper { get; set; }

        public ICollection<OrderDetailResponse> OrderDetails { get; set; }
    }
}
