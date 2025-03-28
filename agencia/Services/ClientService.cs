using agencia.Context;
using agencia.Models.Dto;
using agencia.Models.Entity;
using agencia.Models.Repository.Interfaces;
using agencia.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace agencia.Services
{
    public class ClientService : IClientService
    {
        public readonly IClientRepository _clientRepository;
        public readonly IMapper _mapper;
        public readonly AppDbContext _context;

        public ClientService(IClientRepository clientRepository, IMapper mapper, AppDbContext context)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
            _context = context;
        }

        public async Task<ClientDTO> CreateAsync(ClientDTO clientDTO)
        {
            if (clientDTO == null)
            {
                throw new ArgumentNullException(nameof(clientDTO), "The client object is null.");
            }
            try
            {
                var clientEntity = _mapper.Map<Client>(clientDTO);
                _context.Clients.Add(clientEntity);
                await _context.SaveChangesAsync();
                var clientResponse = _mapper.Map<ClientDTO>(clientEntity);
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

        public async Task<ClientDTO> GetByIdAsync(long Id)
        {
            try
            {
                var clientEntity = await _clientRepository.GetByIdAsync(Id);
                return _mapper.Map<ClientDTO>(clientEntity);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An unexpected error occurred while getting the client.", ex);
            }
        }
        public async Task UpdateAsync(long Id, ClientDTO clientDTO)
        {
            if (clientDTO == null)
            {
                throw new ArgumentNullException(nameof(clientDTO), "The client object is null.");
            }
            try
            {
                var clientEntity = await _clientRepository.GetByIdAsync(Id);
                _mapper.Map(clientDTO, clientEntity);
                await _clientRepository.UpdateAsync(clientEntity);
            }
            catch(KeyNotFoundException ex)
            {
                throw new KeyNotFoundException($"Client with Id {clientDTO.Id} not found.", ex);
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
                await _clientRepository.DeleteAsync(Id);
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

        public async Task<IEnumerable<ClientDTO>> GetAllAsync()
        {
            try
            {
                var clients = await _clientRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<ClientDTO>>(clients);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An unexpected error occurred while getting the clients.", ex);
            }
        }
    }
}
