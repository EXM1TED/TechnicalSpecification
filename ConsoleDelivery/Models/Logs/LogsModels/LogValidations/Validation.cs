using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDelivery.Models.Logs.LogsModels.LogValidations
{
    public class Validation
    {
        public LoggerValidation LoggerValidation { get; set; }
        public static List<ValidationArgs> ValidationsArgs { get; set; } = [];

        public Validation(LoggerValidation loggerValidation)
        {
            LoggerValidation = loggerValidation;
        }
        /// <summary>
        /// Данный метод передает валидацию в список валидаций и делает логгирование списка валидации данных (2 в 1)
        /// </summary>
        /// <param name="validationArgs"></param>
        public void SetAndLogValidation(ValidationArgs validationArgs)
        {
            ValidationsArgs.Add(validationArgs);
            LoggerValidation.Logs = ValidationsArgs;
            Validate();
        }

        private static void Validate()
        {
            LoggerValidation.Log();
        }
    }
}
