using agencia.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace agencia.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Package> Packages { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Configuration of the relationship between the entities

            //Sale-Service (many-to-one)
            modelBuilder.Entity<Sale>()
                .HasOne(s => s.service)
                .WithMany(s => s.sales)
                .HasForeignKey(s => s.serviceId)
                .OnDelete(DeleteBehavior.Restrict);
            //Sale-Package (many-to-one)
            modelBuilder.Entity<Sale>()
                .HasOne(s => s.package)
                .WithMany(s => s.sales)
                .HasForeignKey(s => s.packageId)
                .OnDelete(DeleteBehavior.Restrict);

            //Package-Service (many-to-many)
            modelBuilder.Entity<Package>()
                .HasMany(p => p.services)
                .WithMany(s => s.packages)
                .UsingEntity<Dictionary<string, object>>(
                "Package_Service",
                 c => c.HasOne<Service>().WithMany().HasForeignKey("ServiceId"),
                 c => c.HasOne<Package>().WithMany().HasForeignKey("PackageId")
                );
        }
    }
}
