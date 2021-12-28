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
        try
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<WellReadModel>>("/wells");
        }
        catch
        {
            
        }

        return new List<WellReadModel>();
    }

    public async Task<IEnumerable<LossReadModel>> GetLooses(string wellName)
    {
        try
        {
            var response =
                await _httpClient.GetAsync(
                    $"/looses?wellName={wellName}"); //await _httpClient.GetFromJsonAsync<IEnumerable<LossReadModel>>($"/looses?wellName={wellName}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<LossReadModel>>();
            }
        }
        catch
        {
            
        }

        return new List<LossReadModel>();
    }

    public async Task<LossReadModel> CreateLossRecord(LossWriteModel lossRecord)
    {
        using var response = await _httpClient.PostAsJsonAsync("/loss", lossRecord);
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