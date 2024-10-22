using ConsoleDelivery.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ConsoleDelivery
{
    public class Programm
    {
        public static void Main()
        {
            DataFilter();
        }





        private static void DataFilter()
        {
            int deliveryId = 0;
            double deliveryWeight = 0;
            string? regionName = null;
            int regionId = 0;
            bool validationFaield = false;

            DateTime deliveryDateTime = DateTime.Now;
            Delivery delivery = new Delivery();
            List<Region> regions = [];



            Console.Write("Введите номер заказа: ");
            while (!int.TryParse(Console.ReadLine(), out deliveryId))
            {
                validationFaield = true;
                Console.Write("Пожалуста, введите натуралне число: ");
                LoggerValidation loggerValidationFailed = new LoggerValidation(
                    TypeOfOperation.DeliveryIdInput,
                    validationFaield,
                    DateTime.Now);
                loggerValidationFailed.Log();
            }
            Console.Write($"{validationFaield}");

            while (deliveryId <= 0)
            {
                Console.Write("Пожалуста, введите положительное число отличное от нуля: ");
                while (!int.TryParse(Console.ReadLine(), out deliveryId))
                {
                    Console.Write("Введите число: ");
                }
            }

            Console.Write("Введите вес заказа (в кг): ");
            while (!double.TryParse(Console.ReadLine(), out deliveryWeight))
            {
                Console.Write("Пожалуста, введите число: ");
            }

            while (deliveryWeight <= 0)
            {
                Console.Write("Пожалуста, введите положительное число отличное от нуля: ");
                while (!double.TryParse(Console.ReadLine(), out deliveryWeight))
                {
                    Console.Write("Введите число: ");
                }
            }

            Console.Write("Введите время доставки заказа: ");
            while (!DateTime.TryParse(Console.ReadLine(), out deliveryDateTime))
            {
                Console.Write("Пожалуйста, введите корректную дату");
            };
            
            while(deliveryDateTime < DateTime.Now)
            {
                Console.Write("Пожалуйста, введите корректную дату: ");
                while (!DateTime.TryParse(Console.ReadLine(), out deliveryDateTime))
                {
                    Console.Write("Пожалуйста, введите корректную дату: ");
                };
            }

            Console.Write("Введите название региона: ");
            regionName = Console.ReadLine();
            while (string.IsNullOrEmpty(regionName))
            {
                bool validationFailed = true;
                Console.Write("Пожалуйста, введите название региона: ");
                regionName = Console.ReadLine();
            }

            using (ApplicationContext db = new())
            {
                while (!CheckRegion(regionName, out regions))
                {
                    Console.Write("Такого региона нет, пожалуйста укажите действительный регион: ");
                    regionName = Console.ReadLine();
                }


                foreach (Region region in regions)
                {
                    regionId = region.Id;
                    delivery.Region = region;
                }
            }

            bool CheckRegion(string regionName, out List<Region> regions)
            {
                using (ApplicationContext db = new())
                {
                    try
                    {
                         regions = db.Regions
                            .FromSql($"SELECT RegionId FROM Regions WHERE RegionName = '{regionName}'")
                            .ToList();
                         return true;
                    }
                    catch
                    {
                        regions = null;
                        return false;
                    }
                }
            }
        }
    }
}
