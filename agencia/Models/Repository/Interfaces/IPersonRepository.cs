using agencia.Models.Entity;

namespace agencia.Models.Repository.Interfaces
{
    public interface IPersonRepository
    {
        Task<Person> InsertAsync(Person person);
        Task<Person> GetByIdAsync(long Id);
        Task UpdateAsync(Person person);
        Task DeleteAsync(long Id);
        Task<IEnumerable<Person>> GetAllAsync();
    }
}
