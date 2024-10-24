using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDelivery.Models.Logs.LogsModels
{
    public class Validation
    {
        public DateTime DateTimeOfValidation { get; set; }
        public TypeOfOperation Operation { get; set; }
        public bool ValidationFailed {  get; set; }
        public string? Status { get; set; }
        public string? Discription { get; set; }

        public Validation(TypeOfOperation type, 
            bool validationFailed,
            string discription)
        {
            DateTimeOfValidation = DateTime.Now;
            Operation = type;
            ValidationFailed = validationFailed;
            Discription = discription;

            if (ValidationFailed)
            {
                Status = "Error";
            }
            else
            {
                Status = "OK";
            }
        }
    }
}
