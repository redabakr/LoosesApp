using Looses.Web.Models;
using Looses.Web.Services;
using Microsoft.AspNetCore.Components;

namespace Looses.Web.Pages;

public class IndexBase: ComponentBase
{
    [Inject] public ILoosesApiService LoosesApiService { get; set; }
    public IEnumerable<LossReadModel> LossRecords { get; set; }
    public bool SpinnerVisible { get; set; }
    public  bool ErrorLoading { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            SpinnerVisible = true;
            ErrorLoading = false;
            var wellName = "";
            LossRecords = await LoosesApiService.GetLooses(wellName);
            SpinnerVisible = false;
        }
        catch
        {
            ErrorLoading = true;
            SpinnerVisible = false;
        }
    }
}