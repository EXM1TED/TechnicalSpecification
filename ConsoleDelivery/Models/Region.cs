using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDelivery.Models
{
    public class Region
    {
        public int Id { get; set; }
        public string RegionName { get; set; } = null!;
        public List<Delivery> Deliveries { get; set; } = [];
    }
}
