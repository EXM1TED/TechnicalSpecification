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

        public bool CheckIdDelivery(int deliveryId)
        {
            using (ApplicationContext db = new())
            {
                List<Delivery> deliveries = db.Deliveries
                  .Where(d => d.Id == deliveryId)
                  .ToList();

                return deliveries.Count > 0 ? true : false;
            }
        }
    }
}
