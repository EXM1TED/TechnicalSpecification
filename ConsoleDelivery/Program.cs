using ConsoleDelivery.Models;
using ConsoleDelivery.Models.Logs.LogsModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace ConsoleDelivery
{
    public class Programm
    {
        public static void Main()
        {
            AddDelivery();
        }





        private static void AddDelivery()
        {
            int deliveryId;
            double deliveryWeight;
            string? regionName;
            int regionId;
            bool IsValidationFaield = false;

            DateTime deliveryDateTime = DateTime.Now;
            Delivery delivery = new();
            List<Region> regions = [];
            LoggerValidation loggerValidation = new();
            List<Validation> validations = [];




            Console.Write("Введите номер заказа: ");
            while (!int.TryParse(Console.ReadLine(), out deliveryId))
            {
                IsValidationFaield = true;
                Console.Write("Пожалуста, введите натуралне число: ");

                validations.Add(new Validation(TypeOfOperation.DeliveryIdInput, IsValidationFaield,
                    $"Было введено значение, которое не является натуральном числом)"));
                loggerValidation.Logs = validations;
                loggerValidation.Log();
            }
            while (deliveryId <= 0)
            {
                IsValidationFaield = true;
                Console.Write("Пожалуста, введите положительное число отличное от нуля: ");

                validations.Add(new Validation(TypeOfOperation.DeliveryIdInput, IsValidationFaield,
                    "Введеный Id доставки был ниже нуля"));
                loggerValidation.Logs = validations;
                loggerValidation.Log();

                while (!int.TryParse(Console.ReadLine(), out deliveryId))
                {
                    Console.Write("Введите число: ");

                    validations.Add(new Validation(TypeOfOperation.DeliveryIdInput, true,
                    $"Было введено значение, которое не является натуральном числом)"));
                    loggerValidation.Logs = validations;
                    loggerValidation.Log();
                }
            }
            while (CheckIdDelivery(deliveryId))
            {
                Console.Write("Такой номер заказа уже существует. Пожалуйста, выбирете другой: ");
                while(!int.TryParse(Console.ReadLine(), out deliveryId))
                {
                    Console.Write("Введите число: ");
                }
            }

            validations.Add(new Validation(TypeOfOperation.DeliveryIdInput, false,
                "Был введен корректный Id"));
            loggerValidation.Logs = validations;
            loggerValidation.Log();

            Console.Write("Введите вес заказа (в кг): ");
            while (!double.TryParse(Console.ReadLine(), out deliveryWeight))
            {
                IsValidationFaield = true;
                Console.Write("Пожалуста, введите число: ");
                validations.Add(new Validation(TypeOfOperation.DeliveryWeightInput, IsValidationFaield,
                    "Введенное значение для веса доставки не является натуральным числом"));
                loggerValidation.Logs = validations;
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

            while (!CheckRegion(regionName, out regions))
            {
                Console.Write("Такого региона нет, пожалуйста укажите действительный регион: ");
                regionName = Console.ReadLine();
                validations.Add(new Validation(TypeOfOperation.DeliveryRegionNameInput,
                    IsValidationFaield, "Введенного региона нет в базе данных"));
                loggerValidation.Logs = validations;
                loggerValidation.Log();
            }


            foreach (Region region in regions)
            {
                regionId = region.Id;
                delivery.Region = region;
            }

            static bool CheckRegion(string regionName, out List<Region> regions)
            {
                using(ApplicationContext db = new())
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
                        regions = [];
                        return false;
                    }
                }
            }
        }

        public static bool CheckIdDelivery(int deliveryId)
        {
            bool isDeliveryIdExists = false;
            using (ApplicationContext db = new())
            {
                foreach (Delivery delivery in db.Deliveries.ToList())
                {
                    if (deliveryId == delivery.Id)
                    {
                        isDeliveryIdExists = true;
                    }
                    else
                    {
                        isDeliveryIdExists = false;
                    }
                }

                return isDeliveryIdExists;
            }
        }
    }
}
