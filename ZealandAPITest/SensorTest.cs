using ZealandIdApi.EDbContext;
using ZealandIdApi.Models;

namespace ZealandAPITest
{
    [TestClass]
    public class SensorTest
    {
        

        [TestMethod]
        public void SensorNavn()
        {
            //kravne for et sensornavn er at det ikke m� v�re null og skal v�re minimum 5 karakterer lang
            //V�rdier NUll, 4,5,6 skal derfor testes
            Sensor sensor = new Sensor(null);
            Assert.IsNull(sensor.Navn);

            sensor = new Sensor("bobo");
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => sensor.ValidateNavn());

            sensor = new Sensor("bobob");
            Assert.AreEqual(5, sensor.Navn.Length);

            sensor = new Sensor("bobobo");
            Assert.AreEqual(6, sensor.Navn.Length);

        }
    }
}