using agencia.Models.Dto;

namespace agencia.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeDTO> CreateAsync(EmployeeDTO employeeDTO);
        Task<EmployeeDTO> GetByIdAsync(long Id);
        Task UpdateAsync(long Id, EmployeeDTO employeeDTO);
        Task DeleteAsync(long Id);
        Task<IEnumerable<EmployeeDTO>> GetAllAsync();
    }
}
