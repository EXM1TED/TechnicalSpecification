using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDelivery.Models.ConfigModels
{
    public class ConfigArgs
    {
        public  string? FiltredDataFile { get; set; }
        public  string? ValidationLogsFile { get; set; }
        public  string? OperationLogsFile { get; set; }
    }
}
