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
        public readonly AppDbContext _context;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper, AppDbContext context)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _context = context;
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
                _context.Employees.Add(employeeEntity);
                await _context.SaveChangesAsync();
                var clientResponse = _mapper.Map<EmployeeDTO>(employeeEntity);
                return clientResponse;
            }
            catch (DbUpdateException ex)
            {
                // Captura errores específicos de la base de datos (por ejemplo, violación de restricciones)
                throw new ApplicationException("An error occurred while saving the client to the database.", ex);
            }
            catch (Exception ex)
            {
                // Captura cualquier otra excepción inesperada
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
            catch (Exception ex)
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
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException($"Client with Id {employeeDTO.Id} not found.", ex);
            }
            catch (Exception ex)
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
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException($"Client with Id {Id} not found.", ex);
            }
            catch (Exception ex)
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
