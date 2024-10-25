using Microsoft.VisualStudio.TestPlatform.TestHost;
using ConsoleDelivery;
using ConsoleDelivery.Models;

namespace ConsoleDeliveryTests
{
    [TestClass]
    public class ConsoleDeliveyTest
    {
        [TestMethod]
        public void CheckRegion_From_Programm_Values_Регион_2_True() 
        {
            List<Region> regions = [];
            Assert.IsTrue(Programm.CheckRegion("Регион 1", out regions));
        }
    }
}