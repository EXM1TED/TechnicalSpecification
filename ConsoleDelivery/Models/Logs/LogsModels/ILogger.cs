using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ConsoleDelivery.Models.Logs.LogsModels.LogValidations;

namespace ConsoleDelivery.Models.Logs.LogsModels
{
    public interface ILogger
    {
        public IEnumerable<ValidationArgs> Logs { get; set; }
        void Log();
    }
}
