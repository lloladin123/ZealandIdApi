using ZealandIdApi.EDbContext;

namespace ZealandIdApi.Models
{
    public class Sensor
    {
        public int Id { get; set; }
        public string Navn { get; set; }


        public Sensor(string navn)
        {
            Navn = navn;
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
            return $"{{{nameof(Id)}={Id.ToString()}, {nameof(Navn)}={Navn}}}";
        }

        public override bool Equals(object? obj)
        {
            return obj is Sensor sensor &&
                   Id == sensor.Id &&
                   Navn == sensor.Navn;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
