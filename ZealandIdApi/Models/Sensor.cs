namespace ZealandIdApi.Models
{
    public class Sensor
    {
        public int Id { get; set; }
        public string Navn { get; set; }
        public int LokaleId { get; set; }

        public Sensor(int id, string navn, int lokaleId)
        {
            Id = id;
            Navn = navn;
            LokaleId = lokaleId;
        }

        public Sensor()
        {

        }

        public override string ToString()
        {
            return $"{{{nameof(Id)}={Id.ToString()}, {nameof(Navn)}={Navn}, {nameof(LokaleId)}={LokaleId.ToString()}}}";
        }

        public override bool Equals(object? obj)
        {
            return obj is Sensor sensor &&
                   Id == sensor.Id &&
                   Navn == sensor.Navn &&
                   LokaleId == sensor.LokaleId;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
