using System;
using System.Collections.Generic;
using System.Text;

namespace MyCollection.Common.Models
{
    public class PaymentResponse
    {
        public int Id { get; set; }

        public int Collector { get; set; }

        public int Sale { get; set; }

        public int Customer { get; set; }

        public string Concept { get; set; }

        public string Type { get; set; }

        public DateTime Date { get; set; }

        public DateTime DateLocal => Date.ToLocalTime();

        public double Deposit { get; set; }
    }
}
