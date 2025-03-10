using agencia.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace agencia.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }

    }
}
