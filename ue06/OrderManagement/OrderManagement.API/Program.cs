using System.Text.Json;
using System.Text.Json.Serialization;
using OrderManagement.Logic;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services, builder.Configuration, builder.Environment);

var app = builder.Build();

ConfigureMiddleware(app, app.Environment);
ConfigureEndpoints(app);

app.Run();


// Add services to the container
void ConfigureServices(IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
{
    services.AddControllers(options => options.ReturnHttpNotAcceptable = true) // wenn das options flag nicht gesetzt ist wird immer der default typ im Fehler zur�ckgegeben
        .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); // konvertiert die enum Werte (also 0, 1, 2) auf die wirklichen Namen als string (global)
            }
        )
        .AddXmlDataContractSerializerFormatters();
    // AddScoped erstellt f�r jeden Request ein neues Objekt
    // AddTransient erstellt immer eine neue Instanz (Also auch mehrmals pro Request)
    // AddSingleton erstellt nur ein mal Pro Anwendung ein neues Objekt
    services.AddScoped<IOrderManagementLogic, OrderManagementLogic>();
}

// Configure the HTTP request pipeline
void ConfigureMiddleware(IApplicationBuilder app, IHostEnvironment env) 
{
    app.UseHttpsRedirection();
    app.UseAuthorization();
}

// Configure the routing system
void ConfigureEndpoints(IEndpointRouteBuilder app) 
{
    app.MapControllers();
}