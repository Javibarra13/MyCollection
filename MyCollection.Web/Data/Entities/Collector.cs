using System.Collections.Generic;

namespace MyCollection.Web.Data.Entities
{
    public class Collector
    {
        public int Id { get; set; }

        public User User { get; set; }

        public ICollection<PropertyCollector> PropertyCollectors { get; set; }

        public ICollection<Customer> Customers { get; set; }

        public ICollection<Order> Orders { get; set; }

        public ICollection<Sale> Sales { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}
