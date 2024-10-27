using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleDelivery.Models
{
    
    public class ConfigEditor
    {
        /// <summary>
        /// Данный метод получает данный из файла конфигурации
        /// </summary>
        /// <returns></returns>
        public async static Task SetPathFile(string filePath)
        {
            string directory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string[] path = {directory, "Config.json" };
            string fullPath = Path.Combine(path);
            await using(StreamWriter streamWriter = new(fullPath))
            {
                await using(JsonTextWriter jsonWriter = new(streamWriter))
                {
                    JsonSerializer serializer = new();
                    serializer.NullValueHandling = NullValueHandling.Ignore;
                    serializer.Formatting = Formatting.Indented;
                    serializer.Serialize(jsonWriter, Config.GetConfigInfo());
                }
            }
        }

    }
}
