using Looses.Web.Models;
using Looses.Web.Services;
using Microsoft.AspNetCore.Components;

namespace Looses.Web.Pages;

public class IndexBase: ComponentBase
{
    [Inject] public ILoosesApiService LoosesApiService { get; set; }
    public IEnumerable<WellModel> Wells { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        Wells = await LoosesApiService.GetWells();
    }
}