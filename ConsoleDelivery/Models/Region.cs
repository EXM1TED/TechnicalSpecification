using Microsoft.EntityFrameworkCore;
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

        public static List<Region> GetRegionsList()
        {
            List<Region> regions;
            using (ApplicationContext db = new())
            {
                return regions = db.Regions.ToList();
            }
        }

        /// <summary>
        /// Метод проверяет, есть ли в базе данных регион, который вводит пользователь и возращает список регионов
        /// </summary>
        /// <param name="regionName"></param>
        /// <param name="regions"></param>
        /// <returns></returns>
        public static bool CheckRegion(string regionName, out List<Region> regions)
        {
            using (ApplicationContext db = new())
            {
                regions = db.Regions
                    .FromSql($"SELECT RegionId, RegionName FROM Regions WHERE RegionName = {regionName}")
                    .ToList();
                return regions.Count > 0 ? true : false;
            }
        }

        public static bool CheckRegion(string regionName)
        {
            List <Region> regions;
            using (ApplicationContext db = new())
            {
                regions = db.Regions
                    .FromSql($"SELECT RegionId, RegionName FROM Regions WHERE RegionName = {regionName}")
                    .ToList();
                return regions.Count > 0 ? true : false;
            }
        }
    }
}
