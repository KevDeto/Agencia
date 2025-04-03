using agencia.Context;
using agencia.Models.Entity;
using agencia.Models.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace agencia.Models.Repository
{
    public class ServiceRepository : IServiceRepository
    {
        public readonly AppDbContext _context;

        public ServiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(long Id)
        {
            var service = await _context.Services.FindAsync(Id)
                ?? throw new KeyNotFoundException($"Service with Id {Id} not found.");
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Service>> GetAllAsync()
        {
            return await _context.Services.ToListAsync();
        }

        public async Task<Service> GetByIdAsync(long Id)
        {
            return await _context.Services.FindAsync(Id)
                ?? throw new KeyNotFoundException($"Service with Id {Id} not found.");
        }

        public async Task<Service> InsertAsync(Service service)
        {
            if(service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }
            _context.Services.Add(service);
            await _context.SaveChangesAsync();
            return service;
        }

        public async Task UpdateAsync(Service service)
        {
            if(service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }
            _context.Services.Update(service);
            await _context.SaveChangesAsync();
        }
    }
}
