using agencia.Models.Entity;

namespace agencia.Models.Repository.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> InsertAsync(Employee employee);
        Task<Employee> GetByIdAsync(long Id);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(long Id);
        Task<IEnumerable<Employee>> GetAllAsync();
    }
}
