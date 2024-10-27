using ConsoleDelivery.Models.Logs.LogsModels;
using ConsoleDelivery.Models.Logs.LogsModels.LogOperations;
using ConsoleDelivery.Models.Logs.LogsModels.LogValidations;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ConsoleDelivery.Models.ConfigModels;
using ConsoleDelivery.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ConsoleDelivery.Models.MainOperations.FilterData
{
    public class FilterData
    {
        private static LoggerValidation _loggerValidation { get; set; } = new LoggerValidation();
        private static Validation _validation { get; set; } = new(_loggerValidation);
        private static LoggerOperation _loggerOperation { get; set; } = new();
        private static Operation _operation { get; set; } = new(_loggerOperation);

        public async static void DataFilter()
        {
            string regionName = string.Empty;
            DateTime firsDeliveryDateTime;
            List<Delivery> deliveries = [];

            Console.Write("Введите название региона: ");
            regionName = Console.ReadLine() ?? string.Empty;
            while (string.IsNullOrEmpty(regionName))
            {
                Console.Write("Это поле не должно быть пустым.Пожалуста, укажите название региона: ");
                regionName = Console.ReadLine() ?? string.Empty;

                _validation.SetAndLogValidation(new ValidationArgs(TypeOfOperation.DeliveryRegionNameInput,
                    true, "Название региона не было введено"));
            }

            Console.Write("Введите время первой доставки заказа: ");
            while (!DateTime.TryParse(Console.ReadLine(), out firsDeliveryDateTime))
            {
                Console.Write("Пожалуйста, введите корректную дату");

                _validation.SetAndLogValidation(new ValidationArgs(TypeOfOperation.DeliveryWeightInput, true,
                    "Введенное значение не явялеется датой и времени"));
            };

            while (firsDeliveryDateTime < DateTime.Now)
            {
                _validation.SetAndLogValidation(new ValidationArgs(TypeOfOperation.DeliveryWeightInput, true,
                    "Введенное дата и время являеется устарвшим значением"));

                Console.Write("Пожалуйста, введите корректную дату: ");
                while (!DateTime.TryParse(Console.ReadLine(), out firsDeliveryDateTime))
                {
                    Console.Write("Пожалуйста, введите корректную дату: ");

                    _validation.SetAndLogValidation(new ValidationArgs(TypeOfOperation.DeliveryWeightInput, true,
                    "Введенное значение не явялеется датой и времени"));
                };
            }

            using (ApplicationContext db = new())
            {
                deliveries = db.Deliveries
                    .FromSql($"SELECT * FROM Deliveries WHERE(RegionId = (SELECT RegionId FROM Regions WHERE RegionName = 'Регион 1')) GROUP BY RegionId, TimeOfDelivery HAVING (TimeOfDelivery >= {firsDeliveryDateTime})  AND(TimeOfDelivery <= datetime({firsDeliveryDateTime}, '+30 minutes'))")
                    .ToList();
            }

            if (deliveries.Count > 0)
            {
                string configFilePathValue = Config.CurrentConfigInfo?.FiltredDataFile
                    ?? DataFilePath.GetDefaultFilterDataFile();
                FiltredData filtredData = new FiltredData(regionName,
                firsDeliveryDateTime, deliveries);

                await using (StreamWriter sw = new(configFilePathValue))
                {
                    await using (JsonTextWriter jsonWriter = new(sw))
                    {
                        JsonSerializer jsonSerializer = new();
                        jsonSerializer.Formatting = Formatting.Indented;
                        jsonSerializer.Serialize(sw, filtredData);
                    }
                }
            }
            else
            {
                Console.WriteLine("Таких данных нет");
            }
        }
    }
}

