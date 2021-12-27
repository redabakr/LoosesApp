using Looses.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Looses.Infrastructure.EF.Repositories;

internal sealed class EfLooseRepository: ILooseRepository
{
    private readonly DbSet<Looses.Domain.Entities.Looses> _looses;
    private readonly LoosesDbContext _context;

    public EfLooseRepository(LoosesDbContext context)
    {
        _looses = context.Looses;
        _context = context;
    }

    public async Task<IEnumerable<Domain.Entities.Looses?>> GetAllAsync()
    {
        return await _context.Looses
            .OrderBy(x=>x.LossDate)
            .ToListAsync();
    }

    public async Task<Looses.Domain.Entities.Looses?> GetAsync(int id) =>
       await _looses.Include(x=> x.Well).FirstOrDefaultAsync(x => x.Id == id);
    public async Task AddAsync(Looses.Domain.Entities.Looses looses)
    {
        await _looses.AddAsync(looses);
      //  await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Looses.Domain.Entities.Looses looses)
    {
         _looses.Update(looses);
        //await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Looses.Domain.Entities.Looses looses)
    {
         _looses.Remove(looses);
      //  await _context.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
         await _context.SaveChangesAsync();
    }
}