using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleDelivery.Models
{
    public class LoggerValidation : ILogger
    {
        public DateTime DateTimeOfLog { get; set; }
        public TypeOfOperation Operation { get; set; }
        public bool ValidationFailed { get; set; }
        public string Status { get; set; }
        public LoggerValidation(TypeOfOperation type,
            bool validationFailed,
            DateTime dateTime)
        {
            DateTimeOfLog = dateTime;
            Operation = type;
            ValidationFailed = validationFailed;
        }
        public void Log()
        {
            if (this.ValidationFailed)
            {
                this.Status = "Error";
                using(FileStream fs = new FileStream("LogOut.json", FileMode.Open))
                {
                    JsonSerializer.Serialize<LoggerValidation>(fs, this);
                }
            }
        }
    }
}
