using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCollection.Common.Models
{
    public class CustomerResponse
    {
        public int Id { get; set; }

        //public string Document { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Neighborhood { get; set; }

        public string City { get; set; }

        public string PhoneNumber { get; set; }

        public string PostalCode { get; set; }

        public string Remarks { get; set; }

        public string RefName { get; set; }

        public string RefAddress { get; set; }

        public string RefPhone { get; set; }

        public string RefName2 { get; set; }

        public string RefAddress2 { get; set; }

        public string RefPhone2 { get; set; }

        //public string Status { get; set; }

        public int House { get; set; }

        public int Collector { get; set; }

        public ICollection<CustomerImageResponse> CustomerImages { get; set; }

        //public ICollection<OrderResponse> Orders { get; set; }

        //public ICollection<OrderTmpResponse> OrderTmps { get; set; }

        public ICollection<SaleResponse> Sales { get; set; }

        //public ICollection<SaleTmpResponse> SaleTmps { get; set; }

        //public ICollection<PaymentResponse> Payments { get; set; }

        public string FirstImage => CustomerImages == null || CustomerImages.Count == 0
            ? "https://mycollectionweb.azurewebsites.net/images/CustomerImages/noImage.png"
            : CustomerImages.FirstOrDefault().ImageUrl;
    }
}
