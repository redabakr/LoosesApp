using Looses.Web.Models;
using Looses.Web.Services;
using Microsoft.AspNetCore.Components;

namespace Looses.Web.Pages;

public class IndexBase: ComponentBase
{
    [Inject] public ILoosesApiService LoosesApiService { get; set; }
    public IEnumerable<WellReadModel> WellRecords { get; set; }
    public IEnumerable<LossReadModel> LossRecords { get; set; }
    public bool SpinnerVisible { get; set; }

    protected override async Task OnInitializedAsync()
    {
        SpinnerVisible = true;
        WellRecords = await LoosesApiService.GetWells();
        SpinnerVisible = false;
        var wellName = "";
        LossRecords = await LoosesApiService.GetLooses(wellName);


    }
}