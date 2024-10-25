using Microsoft.VisualStudio.TestPlatform.TestHost;
using ConsoleDelivery;
using ConsoleDelivery.Models;

namespace ConsoleDeliveryTests
{
    [TestClass]
    public class ConsoleDeliveyTest
    {
        [TestMethod]
        public void CheckRegion_From_Programm_Values_������_2_True() 
        {
            List<Region> regions = [];
            Assert.IsTrue(Programm.CheckRegion("������ 1", out regions));
        }
    }
}