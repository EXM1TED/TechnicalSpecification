using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDelivery.Models.Logs.LogsModels
{
    public class LoggerValidation : ILogger
    {
        public IEnumerable<Validation> Logs { get; set; } = [];


        public async void Log()
        {
            await using(StreamWriter sw = new("C:\\Users\\chest\\OneDrive\\Рабочий стол\\Тестовое задание\\TechnicalSpecification\\ConsoleDelivery\\Models\\Logs\\LogsFiles\\LogsValid.json"))
            {
                await using(JsonTextWriter jsonWriter = new JsonTextWriter(sw))
                {
                    JsonSerializer jsonSerializer = new JsonSerializer();
                    jsonSerializer.Formatting = Formatting.Indented;
                    jsonSerializer.Serialize(sw, Logs);
                }
            }

        }
    }
}
