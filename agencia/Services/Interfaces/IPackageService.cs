using agencia.Models.Dto;

namespace agencia.Services.Interfaces
{
    public interface IPackageService
    {
        Task<PackageDTO> CreateAsync(PackageDTO packageDTO);
        Task<PackageDTO> GetByIdAsync(long Id);
        Task UpdateAsync(long Id, PackageDTO packageDTO);
        Task DeleteAsync(long Id);
        Task<IEnumerable<PackageDTO>> GetAllAsync();
    }
}
