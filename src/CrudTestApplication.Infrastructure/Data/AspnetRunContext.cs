using CrudTestApplication.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrudTestApplication.Infrastructure.Data
{
    public class CrudTestApplicationContext : DbContext
    {
        public CrudTestApplicationContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Customer>(ConfigureCustomer);
        }

        private void ConfigureCustomer(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");

            builder.HasKey(ci => ci.Id);         

            builder.Property(cb => cb.FirstName)
                .IsRequired()
                .HasMaxLength(100);
        }

    }
}
