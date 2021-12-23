using Customer.Infrastructure.EF.Config;
using Microsoft.EntityFrameworkCore;

namespace Customer.Infrastructure.EF;

internal sealed class CustomerDbContext: DbContext
{
    public DbSet<Domain.Entities.Customer> Customers { get; set; }
    public CustomerDbContext(DbContextOptions<CustomerDbContext> options): base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("customers");
        var configuration = new CustomerDbConfiguration();
        modelBuilder.ApplyConfiguration<Domain.Entities.Customer>(configuration);
        modelBuilder.ApplyConfiguration<Domain.ValueObjects.ShippingAddress>(configuration);
        base.OnModelCreating(modelBuilder);
    }
}