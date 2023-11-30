namespace ZealandIdApi.Models
{
    public class Sensor
    {
        public int Id { get; set; }
        public string Navn { get; set; }
        public int LokaleId { get; set; }

        public Sensor(string navn, int lokaleId)
        {
            Navn = navn;
            LokaleId = lokaleId;
        }

        public Sensor()
        {

        }

        public void ValidateNavn()
        {
            if (Navn == null)
            {
                throw new ArgumentNullException("Navn må ikke være null");
            }
            if (Navn.Length < 5)
            {
                throw new ArgumentOutOfRangeException("Navn skal mindst være på 5 karakterer");
            }
        }

        public void Validate()
        {
            ValidateNavn();
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
