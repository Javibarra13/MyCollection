using System;
using System.Collections.Generic;
using System.Text;

namespace MyCollection.Common.Models
{
    public class LineResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<SublineResponse> Sublines { get; set; }

        public ICollection<ProductResponse> Products { get; set; }
    }
}
