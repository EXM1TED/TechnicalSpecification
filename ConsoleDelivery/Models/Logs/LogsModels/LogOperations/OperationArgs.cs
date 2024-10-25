using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDelivery.Models.Logs.LogsModels.LogOperations
{
    public class OperationArgs
    {
        public DateTime DateTimeOfValidation { get; set; }
        public TypeOfOperation Operation { get; set; }
        public Delivery? Delivery { get; set; }
        public Region? Region { get; set; }
        public string? Status { get; set; }
        public string? Discription { get; set; }

        public OperationArgs(TypeOfOperation type,
            bool validationFailed,
            string discription)
        {
            DateTimeOfValidation = DateTime.Now;
            Operation = type;
            Discription = discription;
        }
    }
}
