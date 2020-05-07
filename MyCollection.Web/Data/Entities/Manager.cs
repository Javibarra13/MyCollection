using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCollection.Web.Data.Entities
{
    public class Manager
    {
        public int Id { get; set; }

        public User User { get; set; }

        public ICollection<PropertyManager> PropertyManagers { get; set; }

    }
}
