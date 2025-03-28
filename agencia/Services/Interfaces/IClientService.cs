using agencia.Models.Dto;

namespace agencia.Services.Interfaces
{
    public interface IClientService
    {
        Task<ClientDTO> CreateAsync(ClientDTO clientDTO);
        Task<ClientDTO> GetByIdAsync(long Id);
        Task UpdateAsync(long Id, ClientDTO clientDTO);
        Task DeleteAsync(long Id);
        Task<IEnumerable<ClientDTO>> GetAllAsync();
    }
}
