using agencia.Models.Entity;

namespace agencia.Models.Repository.Interfaces
{
    public interface IServiceRepository
    {
        Task<Service> InsertAsync(Service service);
        Task<Service> GetByIdAsync(long Id);
        Task UpdateAsync(Service service);
        Task DeleteAsync(long Id);
        Task<IEnumerable<Service>> GetAllAsync();
    }
}
