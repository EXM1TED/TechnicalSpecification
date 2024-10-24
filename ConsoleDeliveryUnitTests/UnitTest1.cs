using ConsoleDelivery;

namespace ConsoleDeliveryUnitTests
{
    [TestClass]
    public class ConsoleDeliveryTests
    {
        [TestMethod]
        public void CheckIdDelivery_From_IfIdNotExists_Programm_Main() 
        {
            int deliveryId = 999;
            Assert.IsFalse(Programm.CheckIdDelivery(deliveryId));
        }

        [TestMethod]
        public void CheckIdDelivery_From_IfIdExists_Programm_Main()
        {
            int deliveryId = 1;
            Assert.IsTrue(Programm.CheckIdDelivery(deliveryId));
        }
    }
}