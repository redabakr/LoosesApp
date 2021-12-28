using Looses.Application;
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

    public async Task<LossReadDto?> GetlooseDetails(int id)
    {
        var entity = await _looses
            .Include(x => x.Well)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id);
        return entity?.AsDto();
    }

    public async Task<bool> LossRecordForSameDayExistsAsync(string wellName, string eventName, DateTime lossDate)
    {
        return await _looses.AnyAsync(x => x.WellName == wellName && x.EventName == eventName && x.LossDate == lossDate);
    }

    
    public async Task<bool> ExistsWellByNameAsync(string wellName)
    {
        var cacheKey = $"{wellName}-EXISTS";
        var output = _memoryCache.Get<string>(cacheKey);
        if (output is not null) return bool.Parse(output);
        
        var isExist = await _wells.AnyAsync(x => x.Name == wellName);
        _memoryCache.Set(cacheKey, isExist? "true" : "false", TimeSpan.FromMinutes(10));
        return isExist;
    }

    //public async Task<bool> ExistsByEmailAsync(string email) => await _looses.AnyAsync(x => x.Email == email);

    public async Task<IEnumerable<LossReadDto>> GetLooses(string? wellName)
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
            .Select(x => x.AsDto())
            .ToListAsync();
    }

    public async Task<IEnumerable<WellReadDto>> GetWells()
    {
        return await _wells
            .AsNoTracking()
            .Select(x=> new WellReadDto(x.Id, x.Name))
            .ToListAsync();
    }

    public async Task<IEnumerable<LossReadDto>> GetLoosesForWell(string wellName, DateTime previousLossDate)
    {
        var dbQuery = _looses
                .Where(t => t.WellName == wellName && t.LossDate >= previousLossDate);
          
        return await dbQuery.Select(x => x.AsDto()).OrderByDescending(x=> x.LoosDate).ToListAsync();
    }
}