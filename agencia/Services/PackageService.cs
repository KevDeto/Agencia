using agencia.Context;
using agencia.Models.Dto;
using agencia.Models.Entity;
using agencia.Models.Repository;
using agencia.Models.Repository.Interfaces;
using agencia.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace agencia.Services
{
    public class PackageService : IPackageService
    {
        public readonly IMapper _mapper;
        public readonly IPackageRepository _packageRepository;

        public PackageService(IMapper mapper, IPackageRepository packageRepository)
        {
            _mapper = mapper;
            _packageRepository = packageRepository;
        }

        public async Task<PackageDTO> CreateAsync(PackageDTO packageDTO)
        {
            if(packageDTO == null)
            {
                throw new ArgumentNullException(nameof(packageDTO), "The package object is null.");
            }
            try
            {
                var package = _mapper.Map<Package>(packageDTO);

                await _packageRepository.InsertPackageServiceAsync(package, packageDTO.serviceIDs);
                var packageCreated = await _packageRepository.InsertAsync(package);

                return _mapper.Map<PackageDTO>(packageCreated);
            }
            catch(DbUpdateException ex)
            {
                throw new ApplicationException("An error occurred while saving the package to the database.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An unexpected error occurred while creating the package.", ex);
            }
        }

        public async Task DeleteAsync(long Id)
        {
            try 
            {
                await _packageRepository.DeleteAsync(Id);
            }
            catch (Exception ex) when (ex is not KeyNotFoundException)
            {
                throw new ApplicationException("An unexpected error occurred while deleting the package.", ex);
            }
        }

        public async Task<IEnumerable<PackageDTO>> GetAllAsync()
        {
            try
            {
                var packages = await _packageRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<PackageDTO>>(packages);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An unexpected error occurred while getting the packages.", ex);
            }
        }

        public async Task<PackageDTO> GetByIdAsync(long Id)
        {
            try
            {
                var packageEntity = await _packageRepository.GetByIdAsync(Id)
                    ?? throw new KeyNotFoundException($"Package with Id {Id} not found.");

                return _mapper.Map<PackageDTO>(packageEntity);
            }
            catch (Exception ex) when (ex is not KeyNotFoundException)
            {
                throw new ApplicationException("An unexpected error occurred while getting the client.", ex);
            }
        }

        public async Task UpdateAsync(long Id, PackageDTO packageDTO)
        {
            if(packageDTO == null)
            {
                throw new ArgumentNullException(nameof(packageDTO), "The package object is null.");
            }
            try
            {
                var packageEntity = await _packageRepository.GetByIdAsync(Id);
                _mapper.Map(packageDTO, packageEntity);

                await _packageRepository.setPackageServicesAsync(packageEntity, packageDTO.serviceIDs);

                await _packageRepository.UpdateAsync(packageEntity);
            }
            catch (Exception ex) when (ex is not KeyNotFoundException)
            {
                throw new ApplicationException("An unexpected error occurred while updating the package.", ex);
            }
        }
    }
}
