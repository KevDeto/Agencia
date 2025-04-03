namespace agencia.Models.Dto
{
    public class SaleDTO
    {
        public long Id { get; set; }
        public DateTime date { get; set; }
        public double price { get; set; }
        public long serviceId { get; set; }
        public long packageId { get; set; }
    }
}
