using Microsoft.VisualStudio.TestPlatform.TestHost;
using ConsoleDelivery;
using ConsoleDelivery.Models;
using ConsoleDelivery.Models.MainOperations.FilterData;

namespace ConsoleDeliveryTests
{
    [TestClass]
    public class ConsoleDeliveyTest
    {
        [TestMethod]
        public void GetRegionList_From_Region() 
        {
            Assert.IsNotNull(Region.GetRegionsList());
        }

        [TestMethod]
        public void GetDefaultFilterDataFile_From_DataFilePath_Value_FiltredDataJson()
        {
            string expected = "D:\\TechSpec\\ConsoleDeliveryTests\\LogsFiles\\FiltredData.json";
            string actual = DataFilePath.GetDefaultFilterDataFile();

            Assert.AreEqual(expected, actual);   
        }

        [TestMethod]
        public void CheckPath_From_DataFilePath_Value_ConsoleDeliveryTests()
        {
            string expected = "D:\\TechSpec\\ConsoleDeliveryTests\\ConsoleDeliveyTest.cs";
            string actual;
            DataFilePath.CheckPath(expected, out actual);
            Assert.AreEqual(expected, actual);
        }
    }
}