using Microsoft.VisualStudio.TestPlatform.TestHost;
using ConsoleDelivery;
using ConsoleDelivery.Models;
using ConsoleDelivery.Models.MainOperations.FilterData;

namespace ConsoleDeliveryTests
{
    [TestClass]
    public class ConsoleDeliveryTestsFilePath
    {
        [TestMethod]
        public void GetDefaultFilterDataFile_From_DataFilePath_Value_FiltredDataJson()
        {
            string expected = "C:\\Users\\chest\\OneDrive\\Рабочий стол\\Тестовое задание\\TechnicalSpecification\\ConsoleDeliveryTests\\LogsFiles\\FiltredData.json";
            string actual = DataFilePath.GetDefaultFilterDataFile();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetDefaultFilterDataFile_From_DataFilePath_Value_LogsValidJson()
        {
            string expected = "C:\\Users\\chest\\OneDrive\\Рабочий стол\\Тестовое задание\\TechnicalSpecification\\ConsoleDeliveryTests\\LogsFiles\\LogsValid.json";
            string actual = DataFilePath.GetDefaultLogsValid();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetDefaultFilterDataFile_From_DataFilePath_Value_OperationLog()
        {
            string expected = "C:\\Users\\chest\\OneDrive\\Рабочий стол\\Тестовое задание\\TechnicalSpecification\\ConsoleDeliveryTests\\LogsFiles\\OperationsLog.json";
            string actual = DataFilePath.GetDefaultLogsOperationsLog();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckPath_From_DataFilePath_Value_ConsoleDeliveryTests()
        {
            string expected = "C:\\Users\\chest\\OneDrive\\Рабочий стол\\Тестовое задание\\TechnicalSpecification\\ConsoleDelivery\\LogsFiles\\FiltredData.json";
           Assert.IsTrue(DataFilePath.CheckPath(expected, out string actual));
        }

        [TestMethod]
        public void CheckPath_From_DataFilePath_Value_OperationsLog_IsFalse()
        {
            string expected = "C:\\Users\\chest\\OneDrive\\Рабочий стол\\Тестовое задание\\TechnicalSpecification\\ConsoleDelivery\\LogsFiles\\OperationsLog.json";
            Assert.IsTrue(DataFilePath.CheckPath(expected, out string actual));
        }
    }
}
