using CurrencyConverter.Blazor.Client.Services.Generated;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddHttpClient<IConverterService, ConverterService>(client => { client.BaseAddress = new Uri("https://localhost:5001"); }
);

await builder.Build().RunAsync();
