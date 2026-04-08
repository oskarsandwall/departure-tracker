using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// CORS: Allow frontend origin (adjust as needed)
var allowedOrigin = "http://localhost:5500";

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(allowedOrigin)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddHttpClient("SlClient", client =>
{
    // Base URL for SL Real-time API v4 
    client.BaseAddress = new Uri("https://api.sl.se/api2/");
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.Configure<SlSettings>(
    builder.Configuration.GetSection("SlSettings"));

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors("AllowFrontend");
app.MapControllers();

app.Run();

public class SlSettings
{
    public string ApiKey { get; set; } = string.Empty;
    public string SiteId { get; set; } = string.Empty;
}