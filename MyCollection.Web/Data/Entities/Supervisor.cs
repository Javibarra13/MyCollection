using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCollection.Web.Data.Entities
{
    public class Supervisor
    {
        public int Id { get; set; }

        public User User { get; set; }

        public ICollection<PropertySupervisor> PropertySupervisors { get; set; }

    }
}
