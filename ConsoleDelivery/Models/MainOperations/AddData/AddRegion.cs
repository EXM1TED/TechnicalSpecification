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
    public class AddRegion
    {
        private static LoggerValidation _loggerValidation { get; set; } = new();
        private static Validation _validation { get; set; } = new(_loggerValidation);
        private static LoggerOperation _loggerOperation { get; set; } = new();
        private static Operation _operation { get; set; } = new(_loggerOperation);
        public static void CreateNewRegion()
        {
            Console.WriteLine();
            Region region = new();
            List<Region> regions = new List<Region>();

            string regionName = string.Empty;

            Console.Write("Введите название региона: ");
            regionName = Console.ReadLine() ?? string.Empty;
            while (string.IsNullOrEmpty(regionName))
            {
                Console.Write("Это поле не должно быть пустым.Пожалуста, укажите название региона: ");
                regionName = Console.ReadLine() ?? string.Empty;

                _validation.SetAndLogValidation(new ValidationArgs(TypeOfOperation.DeliveryRegionNameInput,
                    true, "Название региона не было введено"));

                while (Region.CheckRegion(regionName))
                {
                    Console.Write("Такое имя региона уже есть. Пожалуйста, ввидете другое: ");
                    regionName = Console.ReadLine();
                }
            }

            _validation.SetAndLogValidation(new ValidationArgs(TypeOfOperation.DeliveryRegionNameInput,
                false, "Было введено коррктное название региона"));

            region.RegionName = regionName;

            using (ApplicationContext db = new())
            {
                db.Regions.Add(region);
                db.SaveChanges();
                _operation.SetAndLogOperation(new OperationArgs(TypeOfOperation.SendNewRegionToDataBase,
                   $"Был добавлен новый регион с названием: {region.RegionName}", null, region));
            }
        }
    }
}
