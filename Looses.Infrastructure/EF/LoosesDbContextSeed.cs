using Looses.Domain.Entities;

namespace Looses.Infrastructure.EF;

internal static class LoosesDbContextSeed
{
    public static async Task SeedWellsDataAsync(LoosesDbContext context)
    {
        // Seed, if necessary
        if (!context.Wells.Any())
        {
            context.Wells.Add(new Well("R-001"));
            context.Wells.Add(new Well("R-002"));
            context.Wells.Add(new Well("R-003"));
            context.Wells.Add(new Well("R-004"));
            context.Wells.Add(new Well("R-005"));

            await context.SaveChangesAsync();
        }
    }
}