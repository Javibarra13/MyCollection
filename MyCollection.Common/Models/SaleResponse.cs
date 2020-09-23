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

        public string TypePayment { get; set; }

        public string DayPayment { get; set; }

        public string Seller { get; set; }

        public int Collector { get; set; }

        public int Customer { get; set; }

        public ICollection<PaymentResponse> Payments { get; set; }

        public ICollection<SaleDetailResponse> SaleDetails { get; set; }
    }
}
