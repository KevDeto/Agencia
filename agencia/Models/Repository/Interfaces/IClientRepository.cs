using agencia.Models.Entity;

namespace agencia.Models.Repository.Interfaces
{
    public interface IClientRepository
    {
        Task<Client> InsertAsync(Client client);
        Task<Client> GetByIdAsync(long Id);
        Task UpdateAsync(Client client);
        Task DeleteAsync(long Id);
        Task<IEnumerable<Client>> GetAllAsync();
    }
}
