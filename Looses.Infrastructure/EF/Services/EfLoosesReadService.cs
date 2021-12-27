using Looses.Application.DTO;
using Looses.Application.Services;
using Looses.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Looses.Infrastructure.EF.Services;

internal sealed class EfLoosesReadService : ILoosesReadService
{
    private readonly DbSet<Looses.Domain.Entities.Looses> _looses;
    private readonly DbSet<Well> _wells;
    private readonly IMemoryCache _memoryCache;

    public EfLoosesReadService(LoosesDbContext context, IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
        _looses = context.Looses;
        _wells = context.Wells;
    }

    public async Task<LoosesDto?> GetlooseDetails(int id)
    {
        var entity = await _looses
            .Include(x => x.Well)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id);
        if (entity is null) return null;
        return AsDto(entity);
    }

    public async Task<bool> ExistsWellByNameAsync(string wellName)
    {
        var cacheKey = $"{wellName}-EXISTS";
        var output = _memoryCache.Get<bool?>(cacheKey);
        if (output is not null) return output.Value;
        
        var isExist = await _wells.AnyAsync(x => x.Name == wellName);
        _memoryCache.Set(cacheKey, isExist, TimeSpan.FromMinutes(10));
        return isExist;
    }

    //public async Task<bool> ExistsByEmailAsync(string email) => await _looses.AnyAsync(x => x.Email == email);

    public async Task<IEnumerable<LoosesDto>> GetLooses(string wellName)
    {
        var dbQuery = _looses
            .Include(x => x.Well)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(wellName))
        {
            dbQuery = dbQuery
                .Where(x => Microsoft.EntityFrameworkCore.EF.Functions.Like(x.WellName, $"%{wellName}%"));
        }

        return await dbQuery
            .AsNoTracking()
            .OrderBy(x=> x.WellName)
            .ThenBy(x=>x.LossDate)
            .Select(x => AsDto(x))
            .ToListAsync();
    }

    public async Task<IEnumerable<LoosesDto>> GetLoosesForWell(string wellName, DateTime previousLossDate)
    {
        var dbQuery = _looses
                .Where(t => t.WellName == wellName && t.LossDate >= previousLossDate);
          
        return await dbQuery.Select(x => AsDto(x)).OrderByDescending(x=> x.LoosDate).ToListAsync();
    }

    private static LoosesDto AsDto(Looses.Domain.Entities.Looses entity)
    {
        return new LoosesDto(
                entity.Id,
                entity.WellName,
                entity.EventName,
                 entity.LossDate,
                entity.DaysOffline
        );
    }
}