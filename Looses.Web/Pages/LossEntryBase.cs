using Looses.Web.Models;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
    
namespace Looses.Web.Pages;

public class LossEntryBase: ComponentBase
{
    [Inject]
    protected HttpClient Http { get; set; }
    protected LossWriteModel LossRecord { get; set; } = new LossWriteModel();
    protected WellReadModel[]? WellRecords;
    protected bool spinnerVisible { get; set; }
    protected bool getWellsError { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        try
        {
            spinnerVisible = true;
            getWellsError = false;
            WellRecords = await Http.GetFromJsonAsync<WellReadModel[]>("wells");
            spinnerVisible = false;
        }
        catch
        {
            spinnerVisible = false;
            getWellsError = true;
        }
    }

    protected async Task HandleValidSubmit()
    {
        // var result = await _LoosesApiService.CreateLossRecord(LossRecord);
        // if (result != null)
        // {
        //     NavigationManager.NavigateTo("/");
        // }
    }
}