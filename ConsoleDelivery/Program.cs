using ConsoleDelivery.Models;
using ConsoleDelivery.Models.Logs.LogsModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace ConsoleDelivery
{
    public class Programm
    {
        private static LoggerValidation _loggerValidation { get; set; } = new();   
        private static List<Validation> _validations { get; set; } = [];

        public static void Main()
        {
            ShowActions();

            int choosedOperation;

            while(!int.TryParse(Console.ReadLine(), out choosedOperation))
            {
                Console.Write("Введите число: ");
                _validations.Add(new Validation(TypeOfOperation.DeliveryChooseAction,
                    true, "При выборе операции, было введено не число"));
            }

            ChooseAction(choosedOperation);
        }

        private static void AddDelivery()
        {
            Console.WriteLine();
            Console.WriteLine("Введите данные о заказе: ");

            int deliveryId;
            double deliveryWeight;
            string? regionName;
            int regionId;

            DateTime deliveryDateTime = DateTime.Now;
            Delivery delivery = new();
            List<Region> regions = [];

            Console.Write("Введите номер заказа: ");
            while (!int.TryParse(Console.ReadLine(), out deliveryId))
            {
                Console.Write("Пожалуста, введите натуралне число: ");

                _validations.Add(new Validation(TypeOfOperation.DeliveryIdInput, true,
                    $"Было введено значение, которое не является натуральном числом)"));
                _loggerValidation.Logs = _validations;
                _loggerValidation.Log();
            }
            while (deliveryId <= 0)
            {
                Console.Write("Пожалуста, введите положительное число отличное от нуля: ");

                _validations.Add(new Validation(TypeOfOperation.DeliveryIdInput, true,
                    "Введеный Id доставки был ниже нуля"));
                _loggerValidation.Logs = _validations;
                _loggerValidation.Log();

                while (!int.TryParse(Console.ReadLine(), out deliveryId))
                {
                    Console.Write("Введите число: ");

                    _validations.Add(new Validation(TypeOfOperation.DeliveryIdInput, true,
                    $"Было введено значение, которое не является натуральном числом)"));
                    _loggerValidation.Logs = _validations;
                    _loggerValidation.Log();
                }
            }
            while (CheckIdDelivery(deliveryId))
            {
                Console.Write("Такой номер заказа уже существует. Пожалуйста, выбирете другой: ");
                while(!int.TryParse(Console.ReadLine(), out deliveryId))
                {
                    Console.Write("Введите число: ");
                }
                _validations.Add(new Validation(TypeOfOperation.DeliveryIdInput, false,
                    "Введенный Id уже указан"));
                _loggerValidation.Logs = _validations;
                _loggerValidation.Log();
            }

            _validations.Add(new Validation(TypeOfOperation.DeliveryIdInput, false,
                "Был введен корректный Id"));
            _loggerValidation.Logs = _validations;
            _loggerValidation.Log();

            Console.Write("Введите вес заказа (в кг): ");
            while (!double.TryParse(Console.ReadLine(), out deliveryWeight))
            {
                Console.Write("Пожалуста, введите число: ");
                _validations.Add(new Validation(TypeOfOperation.DeliveryWeightInput, true,
                    "Введенное значение для веса доставки не является натуральным числом"));
                _loggerValidation.Logs = _validations;
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
                Console.Write("Пожалуйста, введите название региона: ");
                regionName = Console.ReadLine();
            }

            while (!CheckRegion(regionName, out regions))
            {
                Console.Write("Такого региона нет, пожалуйста укажите действительный регион: ");
                regionName = Console.ReadLine();
                _validations.Add(new Validation(TypeOfOperation.DeliveryRegionNameInput,
                    true, "Введенного региона нет в базе данных"));
                _loggerValidation.Logs = _validations;
                _loggerValidation.Log();
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

        private static void AddRegion()
        {
            Console.WriteLine("Для начала, необходимо добавить регион");
            Region region = new Region();

            string regionName = string.Empty;

            Console.Write("Введите название региона: ");
            regionName = Console.ReadLine();

            using(ApplicationContext db = new())
            {
                db.Regions.Add(region);
                db.SaveChanges();
            }
        }

        private static void ChooseAction(int choosedAction)
        {
            switch (choosedAction)
            {
                case 1:
                    AddRegion();
                    AddDelivery();
                    break;

                default:
                    Console.WriteLine("Такой операции не существует");
                    break;
            }
        }

        private static void ShowActions()
        {
            Console.WriteLine("1. Добавить новый заказ");
            Console.WriteLine("2. Отфильтровать данные");
        }
    }
}
