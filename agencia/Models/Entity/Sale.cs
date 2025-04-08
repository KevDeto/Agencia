namespace agencia.Models.Entity
{
    public class Sale
    {
        public long Id { get; set; }
        public DateTime date { get; set; }
        public double price { get; set; }
        //relationship with the Service entity
        public long? serviceId { get; set; }
        public Service? service { get; set; }
        //relationship with the Package entity
        public long? packageId { get; set; }
        public Package? package { get; set; }
    }
}