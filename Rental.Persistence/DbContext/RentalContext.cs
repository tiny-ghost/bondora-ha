using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Rental.Core.Models;

namespace Rental.Persistence
{
    public class RentalContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<RentalItem> RentalItems { get; set; }

        public RentalContext(DbContextOptions<RentalContext> options): base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
                if(!optionsBuilder.IsConfigured)
            {
                var projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new string[] { @"bin\" }, StringSplitOptions.None)[0];
                IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(projectPath).AddJsonFile("appsettings.json").Build();

                optionsBuilder.UseSqlServer(configuration["Database:ConnectionStrings:DefaultConnection"]);
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {

           ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added)
                .ToList()
                .ForEach(x => x.Property("CreatedAt").CurrentValue = DateTime.Now);

            ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Modified)
                .ToList()
                .ForEach(x =>x.Property("ModifiedAt").CurrentValue= DateTime.Now);

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ChangeTracker.Entries()
               .Where(x => x.State == EntityState.Added)
               .ToList()
               .ForEach(x => x.Property("CreatedAt").CurrentValue = DateTime.Now);

            ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Modified)
                .ToList()
                .ForEach(x => x.Property("ModifiedAt").CurrentValue = DateTime.Now);

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

    }
}
