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
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Sale>> GetAllAsync()
        {
            return await _context.Sales
                .Include(s => s.service)
                .Include(s => s.package)
                .ToListAsync();
        }

        public async Task<Sale> GetByIdAsync(long Id)
        {
            return await _context.Sales
                .Include(s => s.service)
                .Include(s => s.package)
                .FirstOrDefaultAsync(s => s.Id == Id)
                ?? throw new KeyNotFoundException($"Sale with Id {Id} not found.");
        }

        public async Task<Package> GetPackageByIdAsync(long? packageID)
        {
            return await _context.Packages.FindAsync(packageID)
                ?? throw new KeyNotFoundException($"Package with Id {packageID} not found.");
        }

        public async Task<Service> GetServiceByIdAsync(long? serviceID)
        {
            return await _context.Services.FindAsync(serviceID)
                ?? throw new KeyNotFoundException($"Service with Id {serviceID} not found.");
        }

        public async Task<Sale> InsertAsync(Sale sale)
        {
            if (sale.serviceId.HasValue && sale.packageId.HasValue)
            {
                throw new InvalidOperationException("Database consistency error: Sale has both service and package");
            }
            if (!sale.serviceId.HasValue && !sale.packageId.HasValue) 
            {
                throw new InvalidOperationException("Database consistency error: Sale has neither service nor package");
            }

            if (sale == null)
            {
                throw new ArgumentNullException(nameof(sale));
            }
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();
            return sale;
        }

        public async Task UpdateAsync(Sale sale)
        {
            if (sale.serviceId.HasValue && sale.packageId.HasValue)
            {
                throw new InvalidOperationException("Database consistency error: Sale has both service and package");
            }
            if (!sale.serviceId.HasValue && !sale.packageId.HasValue)
            {
                throw new InvalidOperationException("Database consistency error: Sale has neither service nor package");
            }

            if (sale == null)
            {
                throw new ArgumentNullException(nameof(sale));
            }
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync();
        }
    }
}
