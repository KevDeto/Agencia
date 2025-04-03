namespace agencia.Models.Entity
{
    public class Package
    {
        public long Id { get; set; }
        public double price { get; set; }
        //one-to-many
        public ICollection<Sale> sales { get; set; }
        //many-to-many
        public ICollection<Service> services { get; set; }
    }
}
