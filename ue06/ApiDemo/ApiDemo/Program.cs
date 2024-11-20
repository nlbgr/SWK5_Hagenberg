var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.

// ################ Minimal API ################

// app.MapGet("/time1", () => DateTime.UtcNow.ToString(@"o"));
// app.MapGet("/time1", () => Results.Content(DateTime.UtcNow.ToString(@"o"), contentType: "text/plain", statusCode: StatusCodes.Status418ImATeapot));
// app.MapGet("/time1", () => Results.Text(DateTime.UtcNow.ToString(@"o")));
app.MapGet("/time1", () => Results.Json(new{Time = DateTime.UtcNow.ToString(@"o")}));

// #############################################

app.MapControllers();
app.Run();
