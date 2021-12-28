using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Looses.Web;
using Looses.Web.Services;
using Syncfusion.Blazor;

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTU1MDU4QDMxMzkyZTM0MmUzMGU5amRrb0hhaithdnpLaTJQc29CS0IwY0JEdHhZOWs3dlQ1MUc3NWUrTDQ9");
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
//builder.Services.AddHttpClient();

//builder.Services.AddScoped<ILoosesApiService, LoosesApiService>();
builder.Services.AddScoped(sp => 
    new HttpClient { BaseAddress = new Uri("https://localhost:3000/") });



builder.Services.AddSyncfusionBlazor();
await builder.Build().RunAsync();
