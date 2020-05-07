using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCollection.Web.Data.Entities
{
    public class Collector
    {
        public int Id { get; set; }

        public User User { get; set; }

        public ICollection<PropertyCollector> PropertyCollectors { get; set; }

        public ICollection<Customer> Customers { get; set; }
    }
}
