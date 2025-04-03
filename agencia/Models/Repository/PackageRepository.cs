using agencia.Context;
using agencia.Models.Dto;
using agencia.Models.Entity;
using agencia.Models.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace agencia.Models.Repository
{
    public class PackageRepository : IPackageRepository
    {
        public readonly AppDbContext _context;

        public PackageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(long Id)
        {
            var package = await _context.Packages
                .Include(p => p.services) // Cargar relaciones para eliminación
                .FirstOrDefaultAsync(p => p.Id == Id)
                ?? throw new KeyNotFoundException($"Package with Id {Id} not found.");

            package.services.Clear(); // Eliminar manualmente las relaciones many-to-many

            _context.Packages.Remove(package);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Package>> GetAllAsync()
        {
            return await _context.Packages
                .Include(p => p.services)
                .ToListAsync(); ;
        }

        public async Task<Package> GetByIdAsync(long Id)
        {
            return await _context.Packages
                .Include(p => p.services)
                .FirstOrDefaultAsync(p => p.Id == Id)
                ?? throw new KeyNotFoundException($"Package with Id {Id} not found.");
        }

        public async Task<Package> InsertAsync(Package package)
        {
            if(package == null)
            {
                throw new ArgumentNullException(nameof(package));
            }
            _context.Packages.Add(package);
            await _context.SaveChangesAsync();
            return package;
        }

        public async Task InsertPackageServiceAsync(Package package, IEnumerable<long> serviceIDs)
        {
            package.services = serviceIDs?.Any() == true
                ? await _context.Services.Where(s => serviceIDs.Contains(s.Id)).ToListAsync()
                : new List<Service>();
        }

        public async Task setPackageServicesAsync(Package package, IEnumerable<long> serviceIDs)
        {
            // Cargar relaciones actuales si no están cargadas
            await _context.Entry(package)
                .Collection(p => p.services)
                .LoadAsync();

            var services = serviceIDs?.Any() == true
                ? await _context.Services.Where(s => serviceIDs.Contains(s.Id)).ToListAsync()
                : new List<Service>();

            package.services = services;
        }

        public async Task UpdateAsync(Package package)
        {
            if(package == null)
            {
                throw new ArgumentNullException(nameof(package));
            }
            _context.Packages.Update(package);
            await _context.SaveChangesAsync();
        }
    }
}
