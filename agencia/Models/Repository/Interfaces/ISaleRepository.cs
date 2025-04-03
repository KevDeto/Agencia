using agencia.Models.Entity;

namespace agencia.Models.Repository.Interfaces
{
    public interface ISaleRepository
    {
        Task<Sale> InsertAsync(Sale sale);
        Task<Sale> GetByIdAsync(long Id);
        Task UpdateAsync(Sale sale);
        Task DeleteAsync(long Id);
        Task<IEnumerable<Sale>> GetAllAsync();
    }
}
