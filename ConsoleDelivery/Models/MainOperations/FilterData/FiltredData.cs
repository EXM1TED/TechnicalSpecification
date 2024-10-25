using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDelivery.Models.MainOperations.FilterData
{
    public class FiltredData
    {
        public string RegionName { get; set; } = null!;
        public DateTime FirstDeliveryDateTime {  get; set; }
        public List<Delivery> Deliveries { get; set; } = [];

        public FiltredData(string regionName,
            DateTime firstDeliveryDateTime,
            List<Delivery> deliveries)
        {
            RegionName = regionName;
            FirstDeliveryDateTime = firstDeliveryDateTime;
            Deliveries = deliveries;
        }
    }
}
