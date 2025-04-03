using agencia.Context;
using agencia.Models.Entity;
using agencia.Models.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace agencia.Models.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(long Id)
        {
            var employee = await _context.Employees.FindAsync(Id)
                ?? throw new KeyNotFoundException($"Employee with Id {Id} not found.");
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> GetByIdAsync(long Id)
        {
            return await _context.Employees.FindAsync(Id)
                ?? throw new KeyNotFoundException($"Employee with Id {Id} not found.");
        }

        public async Task<Employee> InsertAsync(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task UpdateAsync(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }
    }
}
