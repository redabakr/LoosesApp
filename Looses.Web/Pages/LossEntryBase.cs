using Looses.Web.Models;
using Looses.Web.Services;
using Microsoft.AspNetCore.Components;

namespace Looses.Web.Pages;

public class LossEntryBase: ComponentBase
{
    [Inject]
    public ILoosesApiService _LoosesApiService { get; set; }

    public LossWriteModel LossRecord { get; set; } = new LossWriteModel();

    public List<WellReadModel> WellRecords { get; set; } = new List<WellReadModel>();

    public  bool  SpinnerVisible { get; set; }
    public  bool  ErrorLoading { get; set; }
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    protected async override Task OnInitializedAsync()
    {
        try
        {
            SpinnerVisible = true;
            ErrorLoading = false;
            WellRecords = (await _LoosesApiService.GetWells()).ToList();
            SpinnerVisible = false;
        }
        catch
        {
            SpinnerVisible = false;
            ErrorLoading = true;
        }
    }

    protected async Task HandleValidSubmit()
    {
        var result = await _LoosesApiService.CreateLossRecord(LossRecord);
        if (result != null)
        {
            NavigationManager.NavigateTo("/");
        }
    }
}