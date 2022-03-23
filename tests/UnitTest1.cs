using System;
using api.Controllers;
using Xunit;

namespace tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

            Assert.True(true);
            var controller = new WeatherForecastController(new Fakelogger());
        }
    }
}
