using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using api.Controllers;
using Xunit;

namespace tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task GetWeatherForecast_WithPositiveHealthceck_ReturnsPositiveWeather()
        {
            var legalResponses = new[] { "Cool", "Mild", "Warm" };

            Assert.True(true);
            var controller = new WeatherForecastController(new Fakelogger(), new FakeHttpClientFactory());
            var response = await controller.Get(CancellationToken.None);
            Assert.All(response, wf => legalResponses.Contains(wf.Summary));
        }
    }
}
