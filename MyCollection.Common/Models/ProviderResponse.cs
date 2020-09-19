using System;
using System.Collections.Generic;
using System.Text;

namespace MyCollection.Common.Models
{
    public class ProviderResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Neighborhood { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public string RFC { get; set; }

        public string Phone { get; set; }

        public string Contact { get; set; }

        public string UserName { get; set; }

        public string Remarks { get; set; }

        public bool IsAvailable { get; set; }

        public ICollection<ProductResponse> Products { get; set; }
    }
}
