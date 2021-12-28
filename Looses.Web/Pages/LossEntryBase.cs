using Looses.Web.Models;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;

namespace Looses.Web.Pages;

public class LossEntryBase: ComponentBase
{
    [Inject]
    protected HttpClient Http { get; set; }
    // protected LossWriteModel LossRecord { get; set; } = new LossWriteModel();
    protected WellReadModel[]? WellRecords;
    protected bool spinnerVisible { get; set; }
    // protected bool getWellsError { get; set; }

    public List<LossWriteModel> GridData  { get; set; } = new List<LossWriteModel>();
    
    protected override async Task OnInitializedAsync()
    {
      spinnerVisible = true;
      WellRecords = await Http.GetFromJsonAsync<WellReadModel[]>("wells");
      spinnerVisible = false;
    }

    protected async Task PushData()
    {
        var jsonContent = new
        {
            loosesRecords = GridData
        };
        await Http.PutAsJsonAsync("/looses", jsonContent);
    }
}