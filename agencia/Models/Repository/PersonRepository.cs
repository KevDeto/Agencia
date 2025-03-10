using agencia.Context;
using agencia.Models.Entity;
using agencia.Models.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace agencia.Models.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly AppDbContext _context;

        public PersonRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(long Id)
        {
            var person = await _context.Persons.FindAsync(Id)
                ?? throw new KeyNotFoundException($"Person with Id {Id} not found.");

            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _context.Persons.ToListAsync();
        }

        public async Task<Person> GetByIdAsync(long Id)
        {
            return await _context.Persons.FindAsync(Id)
                ?? throw new KeyNotFoundException($"Person with Id {Id} not found.");
        }

        public async Task<Person> InsertAsync(Person person)
        {
            if(person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            return person;
        }

        public async Task UpdateAsync(Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }

            _context.Persons.Update(person);
            await _context.SaveChangesAsync();
        }
    }
}
