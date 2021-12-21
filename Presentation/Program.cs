using Blazored.LocalStorage;
using Fluxor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Presentation;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.InstallInfrastructure();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddFluxor(opt => {
    opt.ScanAssemblies(typeof(Program).Assembly);
    opt.UseRouting();
});

await builder.Build().RunAsync();
