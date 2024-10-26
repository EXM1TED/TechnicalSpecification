using ConsoleDelivery.Models.Logs.LogsModels.LogValidations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDelivery.Models.Logs.LogsModels.LogOperations
{
    public class Operation
    {
        public LoggerOperation LoggerOperation { get; set; } = new();
        public static List<OperationArgs> ListOperations { get; set; } = [];

        public Operation(LoggerOperation loggerOperation)
        {
            LoggerOperation = loggerOperation;
        }

        /// <summary>
        /// Добавляет операцию в список операций, а также его логгирует (2 в 1, работает с анологией SetAndLogValidation() из класса Validation)
        /// </summary>
        /// <param name="operationArgs"></param>
        public void SetAndLogOperation(OperationArgs operationArgs)
        {
            ListOperations.Add(operationArgs);
            LoggerOperation.Logs = ListOperations;
             ValidateOperation();
        }

        private static async void ValidateOperation()
        {
            await LoggerOperation.Log();
        }
    }
}
