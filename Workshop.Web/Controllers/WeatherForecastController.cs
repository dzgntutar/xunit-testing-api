using Microsoft.AspNetCore.Mvc;
using Workshop.Web.Models;

namespace Workshop.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<Student> Get()
    {
        SchoolDbContext db = new SchoolDbContext();

        return db.Student.ToList();
    }
}
