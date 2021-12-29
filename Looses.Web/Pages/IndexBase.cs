using Looses.Web.Models;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;

namespace Looses.Web.Pages;

public class IndexBase: ComponentBase
{
   [Inject]
   protected HttpClient Http { get; set; }

   protected LossReadModel[]? LossRecords;
    protected bool spinnerVisible { get; set; }
    protected bool getLoosesError { get; set; }

    protected override async Task OnInitializedAsync()
    {
            getLoosesError = false;
            spinnerVisible = true;
            LossRecords = await Http.GetFromJsonAsync<LossReadModel[]>("looses");
            spinnerVisible = false;
    }

    protected async Task CalculateOffDays ()
    {
        spinnerVisible = true;
        await Http.PutAsync("/looses/calculate-days-offline", null);
        LossRecords = await Http.GetFromJsonAsync<LossReadModel[]>("looses");
        spinnerVisible = false;
    }
}