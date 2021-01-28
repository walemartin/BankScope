using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarScope.Models
{
    public class Car
    {
        public Guid ID { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public DateTime ShipmentDate { get; set; }
    }
}
