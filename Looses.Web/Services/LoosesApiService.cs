using System.Net.Http.Json;
using Looses.Web.Models;

namespace Looses.Web.Services;

public class LoosesApiService : ILoosesApiService
{
    private readonly HttpClient _httpClient;

    public LoosesApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<IEnumerable<WellModel>> GetWells()
    {
       return await _httpClient.GetFromJsonAsync<IEnumerable<WellModel>>("/wells");
    }

    public Task<IEnumerable<LoosesModel>> GetLooses()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<LoosesModel>> CreateLossRecord()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<LoosesModel>> CreateLossRecords()
    {
        throw new NotImplementedException();
    }
}