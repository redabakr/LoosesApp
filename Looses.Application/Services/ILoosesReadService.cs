using Looses.Application.DTO;

namespace Looses.Application.Services;

public interface ILoosesReadService
{
    Task<LossReadDto?> GetlooseDetails(int Id);

    Task<bool> ExistsWellByNameAsync(string wellName);
    Task<IEnumerable<LossReadDto>> GetLooses(string? wellName );
    Task<IEnumerable<WellReadDto>> GetWells();
    Task<IEnumerable<LossReadDto>> GetLoosesForWell(string wellName, DateTime previousLossDate );
    Task<bool> LossRecordForSameDayExistsAsync(string wellName, string eventName, DateTime lossDate);
}