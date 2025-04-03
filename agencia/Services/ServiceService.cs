using agencia.Context;
using agencia.Models.Dto;
using agencia.Models.Entity;
using agencia.Models.Repository.Interfaces;
using agencia.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace agencia.Services
{
    public class ServiceService : IServiceService
    {
        public readonly IServiceRepository _serviceRepository;
        public readonly IMapper _mapper;

        public ServiceService(IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        public async Task<ServiceDTO> CreateAsync(ServiceDTO serviceDTO)
        {
            if(serviceDTO == null)
            {
                throw new ArgumentNullException(nameof(serviceDTO));
            }
            try
            {
                var serviceEntity = _mapper.Map<Service>(serviceDTO);
                var serviceCreated = await _serviceRepository.InsertAsync(serviceEntity);
                return _mapper.Map<ServiceDTO>(serviceCreated); ;
            }
            catch (DbUpdateException ex)
            {
                throw new ApplicationException("An error occurred while saving the service to the database.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An unexpected error occurred while creating the service.", ex);
            }
        }

        public async Task DeleteAsync(long Id)
        {
            try
            {
                await _serviceRepository.DeleteAsync(Id);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException($"Service with Id {Id} not found.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An unexpected error occurred while deleting the service.", ex);
            }
        }

        public async Task<IEnumerable<ServiceDTO>> GetAllAsync()
        {
            try
            {
                var services = await _serviceRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<ServiceDTO>>(services);
            }
            catch(Exception ex)
            {
                throw new ApplicationException("An unexpected error occurred while getting the services.", ex);
            }
        }

        public async Task<ServiceDTO> GetByIdAsync(long Id)
        {
            try
            {
                var serviceEntity = await _serviceRepository.GetByIdAsync(Id);
                return _mapper.Map<ServiceDTO>(serviceEntity);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An unexpected error occurred while getting the service.", ex);
            }
        }

        public async Task UpdateAsync(long Id, ServiceDTO serviceDTO)
        {
            if(serviceDTO == null)
            {
                throw new ArgumentNullException(nameof(serviceDTO));
            }
            try
            {
                var ServiceEntity = await _serviceRepository.GetByIdAsync(Id);
                _mapper.Map(serviceDTO, ServiceEntity);
                await _serviceRepository.UpdateAsync(ServiceEntity);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException($"Service with Id {Id} not found.", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new ApplicationException("An error occurred while saving the service to the database.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An unexpected error occurred while updating the service.", ex);
            }
        }
    }
}
