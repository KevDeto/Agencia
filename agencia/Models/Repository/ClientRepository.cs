using agencia.Context;
using agencia.Models.Entity;
using agencia.Models.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;

namespace agencia.Models.Repository
{
    public class ClientRepository : IClientRepository
    {
        public readonly AppDbContext _context;

        public ClientRepository(AppDbContext context) 
        {
            _context = context;
        }

        public async Task DeleteAsync(long Id)
        {
            var client = await _context.Clients.FindAsync(Id)
                ?? throw new KeyNotFoundException($"Client with Id {Id} not found.");
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client> GetByIdAsync(long Id)
        {
            return await _context.Clients.FindAsync(Id)
                ?? throw new KeyNotFoundException($"Client with Id {Id} not found.");
        }

        public async Task<Client> InsertAsync(Client client)
        {
            if(client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task UpdateAsync(Client client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }
    }
}
