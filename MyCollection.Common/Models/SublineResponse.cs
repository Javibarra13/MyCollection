using System;
using System.Collections.Generic;
using System.Text;

namespace MyCollection.Common.Models
{
    public class SublineResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public LineResponse Line { get; set; }

        public ICollection<ProductResponse> Products { get; set; }
    }
}
