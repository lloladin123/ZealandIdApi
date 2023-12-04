namespace ZealandIdApi.Models
{
    public class Lokale
    {
        public int Id { get; set; }
        public string Navn { get; set; }

        public Lokale(string navn)
        {
            Navn = navn;
        }

        public Lokale()
        {

        }

        public void ValidateNavn()
        {

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
