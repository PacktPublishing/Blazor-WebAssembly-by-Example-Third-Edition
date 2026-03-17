using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SmartComponents;
using SmartComponents.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IOnnxService, OnnxService>();

var host = builder.Build();

try
{
    var onnx = host.Services.GetRequiredService<IOnnxService>();
    var initialized = await onnx.InitializeAsync();

    if (!initialized)
    {
        Console.Error.WriteLine("Failed to initialize ONNX service");
    }
}
catch (Exception ex)
{
    Console.Error.WriteLine($"ONNX initialization error: {ex.Message}");
}

await host.RunAsync();
