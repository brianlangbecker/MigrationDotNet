using Microsoft.AspNetCore.Mvc;
using MyDotNetWebApplication.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace MyDotNetWebApplication.Controllers;

[ApiController]
[Route("[controller]")]
[AllowAnonymous]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", 
        "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        using var activity = Activity.Current?.Source.StartActivity("GenerateWeatherForecast");
        
        activity?.SetTag("operation.name", "GetWeatherForecast");
        
        _logger.LogInformation("Generating weather forecast");

        var forecast = Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

        activity?.SetTag("forecast.count", forecast.Length);
        
        return forecast;
    }
}
