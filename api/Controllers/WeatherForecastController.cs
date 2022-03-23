using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyNamespace;

namespace api.Controllers
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
        private readonly IHttpClientFactory _factory;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpClientFactory factory)
        {
            _logger = logger;
            _factory = factory;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get(CancellationToken ct)
        {
            var healthCheckClient = new Client(_factory.CreateClient());
            var response = await healthCheckClient.GetHealthCheckAsync(ct);
            var rng = new Random();

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = response.Status == HealthStatus.Healthy ? HealthySummaries[rng.Next(HealthySummaries.Length)] : UnhealthySummaries[rng.Next(UnhealthySummaries.Length)]
            })
            .ToArray();
        }
    }
}
