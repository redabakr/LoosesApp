using Looses.Web.Models;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Components;

namespace Looses.Web.Pages;

public class IndexBase: ComponentBase
{
   [Inject]
   protected HttpClient Http { get; set; }

   private NavigationManager NavigationManager { get; set; }
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
        await Http.PutAsync("/looses/calculate-days-offline", null);
        NavigationManager.NavigateTo("/", forceLoad:true);
    }
}