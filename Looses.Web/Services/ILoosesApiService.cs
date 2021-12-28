using Looses.Web.Models;

namespace Looses.Web.Services;

public interface ILoosesApiService
{
    Task<IEnumerable<WellModel>> GetWells();
    Task<IEnumerable<LoosesModel>> GetLooses();
    Task<IEnumerable<LoosesModel>> CreateLossRecord();
    Task<IEnumerable<LoosesModel>> CreateLossRecords();
}