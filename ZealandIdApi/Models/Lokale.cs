using ZealandIdApi.EDbContext;

namespace ZealandIdApi.Models
{
    public class Lokale
    {
        public int Id { get; set; }
        public string Navn { get; set; }


        public int? SensorId { get; set; }
        public Lokale(string navn, int sensorId)
        {
            Navn = navn;
            SensorId = sensorId;
        }

        public Lokale()
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
            return $"{{{nameof(Id)}={Id.ToString()}, {nameof(Navn)}={Navn}}}";
        }

        public override bool Equals(object? obj)
        {
            return obj is Lokale lokale &&
                   Id == lokale.Id &&
                   Navn == lokale.Navn;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
