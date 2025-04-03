using agencia.Context;
using agencia.Models.Entity;
using agencia.Models.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace agencia.Models.Repository
{
    public class SaleRepository : ISaleRepository
    {
        public readonly AppDbContext _context;

        public SaleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(long Id)
        {
            var sale = await _context.Sales.FindAsync(Id)
                ?? throw new KeyNotFoundException($"Sale with Id {Id} not found.");
            _context.Sales.Remove(sale);
        }

        public async Task<IEnumerable<Sale>> GetAllAsync()
        {
            return await _context.Sales.ToListAsync();
        }

        public async Task<Sale> GetByIdAsync(long Id)
        {
            return await _context.Sales.FindAsync(Id)
                ?? throw new KeyNotFoundException($"Sale with Id {Id} not found.");
        }

        public async Task<Sale> InsertAsync(Sale sale)
        {
            if(sale == null)
            {
                throw new ArgumentNullException(nameof(sale));
            }
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();
            return sale;
        }

        public async Task UpdateAsync(Sale sale)
        {
            if(sale == null)
            {
                throw new ArgumentNullException(nameof(sale));
            }
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync();
        }
    }
}
