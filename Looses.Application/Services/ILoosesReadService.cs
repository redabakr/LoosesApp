using Looses.Application.DTO;

namespace Looses.Application.Services;

public interface ILoosesReadService
{
    Task<LoosesDto?> GetlooseDetails(int Id);

    Task<bool> ExistsWellByNameAsync(string wellName);
    Task<IEnumerable<LoosesDto>> GetLooses(string? wellName );
    Task<IEnumerable<WellDto>> GetWells();
    Task<IEnumerable<LoosesDto>> GetLoosesForWell(string wellName, DateTime previousLossDate );
}