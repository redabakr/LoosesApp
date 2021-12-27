using Looses.Domain.Entities;
using Looses.Infrastructure.EF.Config;
using Microsoft.EntityFrameworkCore;

namespace Looses.Infrastructure.EF;

internal sealed class LoosesDbContext: DbContext
{
    public DbSet<Looses.Domain.Entities.Looses> Looses { get; set; }
    public DbSet<Well> Wells { get; set; }
    public LoosesDbContext(DbContextOptions<LoosesDbContext> options): base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var configuration = new LoosesDbConfiguration();
        modelBuilder.ApplyConfiguration<Looses.Domain.Entities.Looses>(configuration);
        modelBuilder.ApplyConfiguration<Well>(configuration);
        base.OnModelCreating(modelBuilder);
    }
}