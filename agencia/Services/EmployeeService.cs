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
    public class EmployeeService : IEmployeeService
    {
        public readonly IEmployeeRepository _employeeRepository;
        public readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeDTO> CreateAsync(EmployeeDTO employeeDTO)
        {
            if (employeeDTO == null)
            {
                throw new ArgumentNullException(nameof(employeeDTO), "The client object is null.");
            }
            try
            {
                var employeeEntity = _mapper.Map<Employee>(employeeDTO);
                var employeeCreated = await _employeeRepository.InsertAsync(employeeEntity);
                return _mapper.Map<EmployeeDTO>(employeeEntity);
            }
            catch (DbUpdateException ex)
            {
                // Captura errores específicos de la base de datos (por ejemplo, violación de restricciones)
                throw new ApplicationException("An error occurred while saving the client to the database.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An unexpected error occurred while creating the client.", ex);
            }
        }

        public async Task<EmployeeDTO> GetByIdAsync(long Id)
        {
            try
            {
                var employeeEntity = await _employeeRepository.GetByIdAsync(Id);
                return _mapper.Map<EmployeeDTO>(employeeEntity);
            }
            catch (Exception ex) when (ex is not KeyNotFoundException)
            {
                throw new ApplicationException("An unexpected error occurred while getting the client.", ex);
            }
        }

        public async Task UpdateAsync(long Id, EmployeeDTO employeeDTO)
        {
            if (employeeDTO == null)
            {
                throw new ArgumentNullException(nameof(employeeDTO), "The client object is null.");
            }
            try
            {
                var employeeEntity = await _employeeRepository.GetByIdAsync(Id);
                _mapper.Map(employeeDTO, employeeEntity);
                await _employeeRepository.UpdateAsync(employeeEntity);
            }
            catch (Exception ex) when (ex is not KeyNotFoundException)
            {
                throw new ApplicationException("An unexpected error occurred while updating the client.", ex);
            }
        }

        public async Task DeleteAsync(long Id)
        {
            try
            {
                await _employeeRepository.DeleteAsync(Id);
            }
            catch (Exception ex) when (ex is not KeyNotFoundException)
            {
                throw new ApplicationException("An unexpected error occurred while updating the client.", ex);
            }
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAllAsync()
        {
            try
            {
                var employees = await _employeeRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<EmployeeDTO>>(employees);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An unexpected error occurred while getting the clients.", ex);
            }
        }
    }
}
