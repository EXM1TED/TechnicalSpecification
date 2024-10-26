using ConsoleDelivery.Models.Logs.LogsModels.LogValidations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDelivery.Models.Logs.LogsModels.LogOperations
{
    public class LoggerOperation
    {
        public static IEnumerable<OperationArgs> Logs { get; set; } = [];

        public async static Task Log()
        {
            string filePath = DataFilePath.LogsOperationsFile ??
                DataFilePath.GetDefaultLogsOperationsLog();

            await using (StreamWriter sw = new(filePath))
            {
                await using (JsonTextWriter jsonWriter = new(sw))
                {
                    JsonSerializer jsonSerializer = new();
                    jsonSerializer.Formatting = Formatting.Indented;
                    jsonSerializer.NullValueHandling = NullValueHandling.Ignore;
                    jsonSerializer.DefaultValueHandling = DefaultValueHandling.Ignore;
                    jsonSerializer.Serialize(sw, Logs);
                }
            }
        }
    }
}
