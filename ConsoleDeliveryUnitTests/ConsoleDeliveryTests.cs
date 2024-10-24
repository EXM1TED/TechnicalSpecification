using ConsoleDelivery;

namespace ConsoleDeliveryUnitTests
{
    [TestClass]
    public class ConsoleDeliveryTests
    {
        [TestMethod]
        public void CheckIdDelivery_IfIdNotExists_From_Programm_Main() 
        {
            int deliveryId = 999;
            Assert.IsFalse(Programm.CheckIdDelivery(deliveryId));
        }

        [TestMethod]
        public void CheckIdDelivery_IfIdExists_From_Programm_Main()
        {
            int deliveryId = 1;
            Assert.IsTrue(Programm.CheckIdDelivery(deliveryId));
        }
    }
}