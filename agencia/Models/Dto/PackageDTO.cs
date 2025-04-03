namespace agencia.Models.Dto
{
    public class PackageDTO
    {
        public long Id { get; set; }
        public double price { get; set; }
        public ICollection<long> serviceIDs { get; set; }
    }
}
