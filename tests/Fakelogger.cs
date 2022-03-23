using System;
using api.Controllers;
using Microsoft.Extensions.Logging;

namespace tests
{
    public class Fakelogger: ILogger<WeatherForecastController>
    {
        private class FakeScope: IDisposable {
            public void Dispose() { }
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return new FakeScope();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return false;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter) { }
    }
}
