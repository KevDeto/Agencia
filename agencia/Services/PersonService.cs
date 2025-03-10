using agencia.Context;
using agencia.Models.Dto;
using agencia.Models.Entity;
using agencia.Models.Repository;
using agencia.Models.Repository.Interfaces;
using agencia.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace agencia.Services
{
    public class PersonService : IPersonService
    {
        public readonly IPersonRepository _personRepository;
        public readonly IMapper _mapper;
        public readonly AppDbContext _context;

        public PersonService(IPersonRepository personRepository, IMapper mapper, AppDbContext context)
        {
            _personRepository = personRepository;
            _mapper = mapper;
            _context = context;
        }

        public async Task<PersonDTO> CreateAsync(PersonDTO personDTO)
        {
            if(personDTO == null)
            {
                throw new ArgumentNullException(nameof(personDTO), "The person object is null.");
            }
            try
            {
                var personEntity = _mapper.Map<Person>(personDTO);

                _context.Persons.Add(personEntity);
                await _context.SaveChangesAsync();

                var personResponse = _mapper.Map<PersonDTO>(personEntity);
                return personResponse;
            }
            catch (DbUpdateException ex)
            {
                // Captura errores específicos de la base de datos (por ejemplo, violación de restricciones)
                throw new ApplicationException("An error occurred while saving the person to the database.", ex);
            }
            catch (Exception ex)
            {
                // Captura cualquier otra excepción inesperada
                throw new ApplicationException("An unexpected error occurred while creating the person.", ex);
            }
        }

        public async Task<PersonDTO> GetByIdAsync(long Id)
        {
            try
            {
                var person = await _personRepository.GetByIdAsync(Id);
                return _mapper.Map<PersonDTO>(person);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException($"Person with Id {Id} not found.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving the person.", ex);
            }
        }

        public async Task DeleteAsync(long Id)
        {
            try
            {
                await _personRepository.DeleteAsync(Id);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException($"Person with Id {Id} not found.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while deleting the person.", ex);
            }
        }

        public async Task<IEnumerable<PersonDTO>> GetAllAsync()
        {
            try
            {
                var persons = await _personRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<PersonDTO>>(persons);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving all persons.", ex);
            }
        }



        public async Task UpdateAsync(PersonDTO personDTO)
        {
            if (personDTO == null)
            {
                throw new ArgumentNullException(nameof(personDTO), "The personDTO object cannot be null.");
            }
            try
            {
                var person = _mapper.Map<Person>(personDTO);
                await _personRepository.UpdateAsync(person);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException($"Person with Id {personDTO.Id} not found.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while updating the person.", ex);
            }
        }
    }
}
