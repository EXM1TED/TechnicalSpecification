using ConsoleDelivery.Models.ConfigModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDelivery.Models.Logs.LogsModels.LogValidations
{
    public class LoggerValidation 
    {
        public static IEnumerable<ValidationArgs> Logs { get; set; } = [];


        public static async void Log()
        {
            Config.GetCurrentConfigInfo();
            string filePath = Config.CurrentConfigInfo?.ValidationLogsFile
                ?? DataFilePath.GetDefaultLogsValid();

            await using (StreamWriter sw = new(filePath))
            {
                await using (JsonTextWriter jsonWriter = new(sw))
                {
                    JsonSerializer jsonSerializer = new();
                    jsonSerializer.Formatting = Formatting.Indented;
                    jsonSerializer.Serialize(sw, Logs);
                }
            }

        }
    }
}
