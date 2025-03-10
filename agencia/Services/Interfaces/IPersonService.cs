using agencia.Models.Dto;

namespace agencia.Services.Interfaces
{
    public interface IPersonService
    {
        Task<PersonDTO> CreateAsync(PersonDTO personDTO);
        Task<PersonDTO> GetByIdAsync(long Id);
        Task UpdateAsync(PersonDTO personDTO);
        Task DeleteAsync(long Id);
        Task<IEnumerable<PersonDTO>> GetAllAsync();
    }
}
