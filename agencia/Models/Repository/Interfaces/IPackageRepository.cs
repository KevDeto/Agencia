using agencia.Models.Entity;

namespace agencia.Models.Repository.Interfaces
{
    public interface IPackageRepository
    {
        Task<Package> InsertAsync(Package package);
        Task InsertPackageServiceAsync(Package package, IEnumerable<long> services);
        Task<Package> GetByIdAsync(long Id);
        Task UpdateAsync(Package package);
        Task setPackageServicesAsync(Package package, IEnumerable<long> serviceIDs);
        Task DeleteAsync(long Id);
        Task<IEnumerable<Package>> GetAllAsync();
    }
}
