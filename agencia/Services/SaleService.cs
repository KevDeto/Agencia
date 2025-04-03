using agencia.Models.Dto;
using agencia.Services.Interfaces;

namespace agencia.Services
{
    public class SaleService : ISaleService
    {
        public Task<SaleDTO> CreateAsync(SaleDTO saleDTO)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(long Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SaleDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SaleDTO> GetByIdAsync(long Id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(long Id, SaleDTO saleDTO)
        {
            throw new NotImplementedException();
        }
    }
}
