using ConsoleDelivery.Models.Logs.LogsModels.LogValidations;
using ConsoleDelivery.Models.Logs.LogsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleDelivery.Models.Logs.LogsModels.LogOperations;

namespace ConsoleDelivery.Models.MainOperations.AddData
{
    public class AddDelivery
    {
        private static LoggerValidation _loggerValidation { get; set; } = new();
        private static Validation _validation { get; set; } = new(_loggerValidation);
        private static LoggerOperation _loggerOperation { get; set; } = new();
        private static Operation _operation { get; set; } = new(_loggerOperation);

        public static void CreateNewDelivery()
        {
            Console.WriteLine();
            Console.WriteLine("Введите данные о заказе: ");

            int deliveryId;
            double deliveryWeight;
            string? regionName;

            DateTime deliveryDateTime = DateTime.Now;
            Delivery delivery = new();
            List<Region> regions;

            Console.Write("Введите номер заказа: ");
            while (!int.TryParse(Console.ReadLine(), out deliveryId))
            {
                Console.Write("Пожалуста, введите натуралне число: ");

                _validation.SetAndLogValidation(new ValidationArgs(TypeOfOperation.DeliveryIdInput, true,
                    $"Было введено значение, которое не является натуральном числом)"));
            }
            while (deliveryId <= 0)
            {
                Console.Write("Пожалуста, введите положительное число отличное от нуля: ");

                _validation.SetAndLogValidation(new ValidationArgs(TypeOfOperation.DeliveryIdInput, true,
                    "Введеный Id доставки был ниже нуля"));

                while (!int.TryParse(Console.ReadLine(), out deliveryId))
                {
                    Console.Write("Введите число: ");

                    _validation.SetAndLogValidation(new ValidationArgs(TypeOfOperation.DeliveryIdInput, true,
                    $"Было введено значение, которое не является натуральном числом)"));
                }
            }
            while (delivery.CheckIdDelivery(deliveryId))
            {
                Console.Write("Такой номер заказа уже существует. Пожалуйста, выбирете другой: ");
                while (!int.TryParse(Console.ReadLine(), out deliveryId))
                {
                    Console.Write("Введите число: ");
                }

                _validation.SetAndLogValidation(new ValidationArgs(TypeOfOperation.DeliveryIdInput, false,
                    "Введенный Id уже указан"));
            }

            _validation.SetAndLogValidation(new ValidationArgs(TypeOfOperation.DeliveryIdInput, false,
                "Был введен корректный Id"));

            Console.Write("Введите вес заказа (в кг): ");
            while (!double.TryParse(Console.ReadLine(), out deliveryWeight))
            {
                Console.Write("Пожалуста, введите натуральное число: ");

                _validation.SetAndLogValidation(new ValidationArgs(TypeOfOperation.DeliveryWeightInput, true,
                    "Введенное значение для веса доставки не является натуральным числом"));
            }

            while (deliveryWeight <= 0)
            {
                Console.Write("Пожалуста, введите положительное число отличное от нуля: ");

                _validation.SetAndLogValidation(new ValidationArgs(TypeOfOperation.DeliveryWeightInput, true,
                    "Введенное значение для веса доставки не является натуральным числом"));

                while (!double.TryParse(Console.ReadLine(), out deliveryWeight))
                {
                    Console.Write("Введите число: ");

                    _validation.SetAndLogValidation(new ValidationArgs(TypeOfOperation.DeliveryWeightInput, true,
                    "Введенное значение не ялвяется числом"));
                }
            }

            Console.Write("Введите время доставки заказа: ");
            while (!DateTime.TryParse(Console.ReadLine(), out deliveryDateTime))
            {
                Console.Write("Пожалуйста, введите корректную дату");

                _validation.SetAndLogValidation(new ValidationArgs(TypeOfOperation.DeliveryWeightInput, true,
                    "Введенное значение не явялеется датой и времени"));
            };

            while (deliveryDateTime < DateTime.Now)
            {
                _validation.SetAndLogValidation(new ValidationArgs(TypeOfOperation.DeliveryWeightInput, true,
                    "Введенное дата и время являеется устарвшим значением"));

                Console.Write("Пожалуйста, введите корректную дату: ");
                while (!DateTime.TryParse(Console.ReadLine(), out deliveryDateTime))
                {
                    Console.Write("Пожалуйста, введите корректную дату: ");

                    _validation.SetAndLogValidation(new ValidationArgs(TypeOfOperation.DeliveryWeightInput, true,
                    "Введенное значение не явялеется датой и времени"));
                };
            }

            Console.Write("Введите название региона: ");
            regionName = Console.ReadLine();
            while (string.IsNullOrEmpty(regionName))
            {
                Console.Write("Пожалуйста, введите название региона: ");
                regionName = Console.ReadLine();
            }

            while (!Region.CheckRegion(regionName, out regions))
            {
                Console.Write("Такого региона нет, пожалуйста укажите действительный регион: ");
                regionName = Console.ReadLine();

                _validation.SetAndLogValidation(new ValidationArgs(TypeOfOperation.DeliveryRegionNameInput,
                    true, "Введенного региона нет в базе данных"));
            }


            foreach (Region _region in regions)
            {
                delivery.RegionId = _region.Id;
            }
            delivery.Weight = deliveryWeight;
            delivery.TimeOfDelivery = deliveryDateTime;

            using (ApplicationContext db = new())
            {
                db.Deliveries.Add(delivery);
                db.SaveChanges();

                _operation.SetAndLogOperation(new OperationArgs(TypeOfOperation.SendNewDeliveryToDataBase,
                    $"Был добавлен новый заказ", delivery, null));
            }
        }
    }
}
