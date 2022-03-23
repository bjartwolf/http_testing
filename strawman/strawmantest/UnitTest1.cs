using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using strawmanapi.Controllers;
using strawmanapi.HealthCheckService;
using Xunit;

namespace strawmantest
{
    public class WeatherForecastTests
    {
        [Fact]
        public async Task GetWeatherForecast_WithPositiveHealthCheck_ReturnsPositiveWeather()
        {
            var legalResponses = new[] { "Cool", "Mild", "Warm" };

            var logger = new Mock<ILogger<WeatherForecastController>>();

            var service = new Mock<IHealthCheckService>();
            service.Setup(x => x.GetHealth(It.IsAny<CancellationToken>())).Returns(
                Task.FromResult(new HealthDto { HealthStatus = HealthStatus.Healthy}));
            var controller = new WeatherForecastController(logger.Object, service.Object);
            var response = await controller.Get(CancellationToken.None);

            Assert.All(response, wf => Assert.Contains(wf.Summary, legalResponses));
        }

        [Fact]
        public async Task GetWeatherForecast_WithNegativeHealthCheck_ReturnsNegativeWeather()
        {
            var legalResponses = new[] { "Freezing", "Bracing", "Chilly", "Balmy", "Hot", "Sweltering", "Scorching" };

            var logger = new Mock<ILogger<WeatherForecastController>>();

            var service = new Mock<IHealthCheckService>();
            service.Setup(x => x.GetHealth(It.IsAny<CancellationToken>())).Returns(
                Task.FromResult(new HealthDto { HealthStatus = HealthStatus.Failed }));
            var controller = new WeatherForecastController(logger.Object, service.Object);
            var response = await controller.Get(CancellationToken.None);

            Assert.All(response, wf => Assert.Contains(wf.Summary, legalResponses));
        }
    }
}
