using Looses.Domain.Entities;

namespace Looses.Infrastructure.EF;

internal static class LoosesDbContextSeed
{
    public static async Task SeedWellsDataAsync(LoosesDbContext context)
    {
        // Seed, if necessary
        if (!context.Wells.Any())
        {
            context.Wells.Add(new Well(1,"R-001"));
            context.Wells.Add(new Well(2,"R-002"));
            context.Wells.Add(new Well(3,"R-003"));
            context.Wells.Add(new Well(4,"R-004"));
            context.Wells.Add(new Well(5,"R-005"));

            await context.SaveChangesAsync();
        }
    }
}