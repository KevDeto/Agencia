namespace agencia.Models.Entity
{
    public class Service
    {
        public long Id { get; set; }
        public String name { get; set; }
        public String description { get; set; }
        public String destiny { get; set; }
        public DateOnly date { get; set; }
        public double price { get; set; }
        //one-to-many
        public ICollection<Sale> sales { get; set; }
        //many-to-many
        public ICollection<Package> packages { get; set; }

    }
}
