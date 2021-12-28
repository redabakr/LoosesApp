using Looses.Web.Models;

namespace Looses.Web.Services;

public interface ILoosesApiService
{
    Task<IEnumerable<WellReadModel>> GetWells();
    Task<IEnumerable<LossReadModel>> GetLooses(string wellName);
    Task<LossReadModel> CreateLossRecord(LossWriteModel lossRecord);
    Task<bool> CreateLossRecords(IEnumerable<LossWriteModel> lossRecords);
    Task<bool> CalculateOfflineDays();
}