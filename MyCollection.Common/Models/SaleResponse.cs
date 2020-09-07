using System;
using System.Collections.Generic;
using System.Text;

namespace MyCollection.Common.Models
{
    public class SaleResponse
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime StartDateLocal => StartDate.ToLocalTime();

        public DateTime EndDateLocal => EndDate.ToLocalTime();

        public double Payment { get; set; }

        public double Deposit { get; set; }

        public string Remarks { get; set; }

        //public ICollection<PaymentResponse> Payments { get; set; }

        public string Warehouse { get; set; }

        public string House { get; set; }

        public CollectorResponse Collector { get; set; }

        public string TypePayment { get; set; }

        public string DayPayment { get; set; }

        public string Seller { get; set; }

        public CustomerResponse Customer { get; set; }

        public string State { get; set; }

        public ICollection<SaleDetailResponse> SaleDetails { get; set; }
    }
}
