using ZealandIdApi.Models;

namespace ZealandAPITest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SesnorNavn()
        {
            //kravne for et sensornavn er at det ikke må være null og skal være minimum 5 karakterer lang
            //Værdier NUll, 4,5,6 skal derfor testes
            Sensor sensor = new Sensor(null, 1);
            Assert.IsNull(sensor.Navn);

            sensor = new Sensor("bobo", 1);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => sensor.ValidateNavn());

            sensor = new Sensor("bobob", 1);
            Assert.AreEqual(5, sensor.Navn.Length);

            sensor = new Sensor("bobobo", 1);
            Assert.AreEqual(6, sensor.Navn.Length);

        }
    }
}