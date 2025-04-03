using agencia.Models.Dto;

namespace agencia.Services.Interfaces
{
    public interface IServiceService
    {
        Task<ServiceDTO> CreateAsync(ServiceDTO serviceDTO);
        Task<ServiceDTO> GetByIdAsync(long Id);
        Task UpdateAsync(long Id, ServiceDTO serviceDTO);
        Task DeleteAsync(long Id);
        Task<IEnumerable<ServiceDTO>> GetAllAsync();
    }
}
