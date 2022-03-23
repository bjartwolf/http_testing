using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using strawmanapi.HealthCheckService;

namespace strawmanapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private static readonly string[] HealthySummaries = new[]
        {
            "Cool", "Mild", "Warm"
        };

        private static readonly string[] UnhealthySummaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IHealthCheckService _healthCheckService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHealthCheckService healthCheckService)
        {
            _logger = logger;
            _healthCheckService = healthCheckService;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get(CancellationToken ct)
        {
            var response = await _healthCheckService.GetHealth(ct);
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = response.HealthStatus == HealthStatus.Healthy ? HealthySummaries[rng.Next(HealthySummaries.Length)] : UnhealthySummaries[rng.Next(UnhealthySummaries.Length)]
            })
            .ToArray();
        }
    }
}
