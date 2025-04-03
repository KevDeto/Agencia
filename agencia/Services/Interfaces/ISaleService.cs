using agencia.Models.Dto;

namespace agencia.Services.Interfaces
{
    public interface ISaleService
    {
        Task<SaleDTO> CreateAsync(SaleDTO saleDTO);
        Task<SaleDTO> GetByIdAsync(long Id);
        Task UpdateAsync(long Id, SaleDTO saleDTO);
        Task DeleteAsync(long Id);
        Task<IEnumerable<SaleDTO>> GetAllAsync();
    }
}
