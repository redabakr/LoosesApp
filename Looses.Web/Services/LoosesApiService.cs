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
    public async Task<IEnumerable<WellReadModel>> GetWells()
    {
       return await _httpClient.GetFromJsonAsync<IEnumerable<WellReadModel>>("/wells");
    }

    public async Task<IEnumerable<LossReadModel>> GetLooses(string wellName)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<LossReadModel>>($"/looses?wellName={wellName}");
    }

    public async Task<LossReadModel> CreateLossRecord(LossWriteModel lossRecord)
    {
        var postBody = new { Title = "Blazor POST Request Example" };

        using var response = await _httpClient.PostAsJsonAsync("/loss", postBody);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<LossReadModel>();
        }
        return null;
    }

    public Task<bool> CreateLossRecords(IEnumerable<LossWriteModel> lossRecords)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CalculateOfflineDays()
    {
        var response = await _httpClient.PutAsync("/calculate-days-offline", null);
        return response.IsSuccessStatusCode;
    }
}