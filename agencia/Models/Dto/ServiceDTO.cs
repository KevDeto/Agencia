namespace agencia.Models.Dto
{
    public class ServiceDTO
    {
        public long Id { get; set; }
        public String name { get; set; }
        public String description { get; set; }
        public String destiny { get; set; }
        public DateOnly date { get; set; }
        public double price { get; set; }
    }
}
