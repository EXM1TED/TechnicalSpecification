using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleDelivery.Models.Logs.LogsModels
{
    public interface ILogger
    {
        public IEnumerable<Validation> Logs { get; set; }
        void Log();
    }
}
