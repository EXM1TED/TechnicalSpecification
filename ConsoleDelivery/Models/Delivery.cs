using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDelivery.Models
{
    public class Delivery
    {
        public int Id { get; set; }
        public double Weight { get; set; }
        public int RegionId { get; set; } 
        public Region? Region { get; set; }
        public DateTime TimeOfDelivery { get; set; }
    }
}
