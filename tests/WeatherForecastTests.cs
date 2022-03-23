using System.Threading;
using System.Threading.Tasks;
using api.Controllers;
using Xunit;

namespace tests
{
    public class WeatherForecastTests
    {
        [Fact]
        public async Task GetWeatherForecast_WithPositiveHealthCheck_ReturnsPositiveWeather()
        {
            var legalResponses = new[] { "Cool", "Mild", "Warm" };

            var controller = new WeatherForecastController(new Fakelogger(), new FakeHttpClientFactoryOk());
            var response = await controller.Get(CancellationToken.None);
            Assert.All(response, wf => Assert.Contains(wf.Summary, legalResponses));
        }

        [Fact]
        public async Task GetWeatherForecast_WithNegativeHealthCheck_ReturnsNegativeWeather()
        {
            var legalResponses = new[] {"Freezing", "Bracing", "Chilly", "Balmy", "Hot", "Sweltering", "Scorching"};
            var controller = new WeatherForecastController(new Fakelogger(), new FakeHttpClientFactoryFail());
            var response = await controller.Get(CancellationToken.None);
            Assert.All(response, wf => Assert.Contains(wf.Summary, legalResponses));
        }
    }
}
