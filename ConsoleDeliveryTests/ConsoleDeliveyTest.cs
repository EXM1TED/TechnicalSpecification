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
    }
}