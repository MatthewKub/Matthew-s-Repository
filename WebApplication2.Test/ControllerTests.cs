using Microsoft.Extensions.Logging;
using WebApplication2.Controllers;
using NSubstitute;

namespace WebApplication2.Test
{
    public class ControllerTests
    {
        [Fact]
        public void ControllerConstructorWorks()
        {
            // Setup

            var logger = Substitute.For<ILogger<WeatherForecastController>>();

            // Act 
            var controller = new WeatherForecastController(logger);

            // Assert 
            Assert.True(controller != null);
        }

        [Fact]
        public void ControllerGet_ReturnSucess()
        {
            // Setup
            var logger = Substitute.For<ILogger<WeatherForecastController>>();
            var controller = new WeatherForecastController(logger);

            // Act 
            var result = controller.Get();

            // Assert 
            var a = result.ToArray();
            Assert.True(a.Length == 5);
            var e2 = a[1];
            Assert.True(e2.Date.Hour == DateTime.Now.AddDays(2).Hour);
            Assert.True(e2.TemperatureC > -20 && e2.TemperatureC < 55);
            Assert.True(e2.Summary != null);
        }

        [Fact]
        public void ControllerAdd_ReturnSucess()
        {
            // Setup
            var logger = Substitute.For<ILogger<WeatherForecastController>>();
            var controller = new WeatherForecastController(logger);
            int x = 1;
            int y = 2;

            // Act 
            var result = controller.Add(x, y);

            // Assert 
            Assert.True(result == 3);

        }

        [Fact]
        public void ControllerAdd_ThrowsException()
        {
            // Setup
            ILogger<WeatherForecastController> logger = null;
            var controller = new WeatherForecastController(logger);
            int x = 1;
            int y = 2;

            // Act 
            Assert.Throws<ArgumentNullException>(() => controller.Add(x, y));

        }

        [Fact]
        public void ControllerAdd_LogsAsExpected()
        {
            // Setup
            var logger = Substitute.For<MockLogger<WeatherForecastController>>();

            var controller = new WeatherForecastController(logger);
            int x = 3;
            int y = 4;

            // Act 
            var result = controller.Add(x, y);

            // Assert 
            Assert.True(result == 7);
            logger.Received()
               .Log(LogLevel.Information, Arg.Is<string>(s => s.Contains("x = 3 y = 4"))); 
        }
    }

    public abstract class MockLogger <T> : ILogger <T>
    {
        void ILogger.Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter) =>
            Log(logLevel, formatter(state, exception));

        public abstract void Log(LogLevel logLevel, string message);

        public virtual bool IsEnabled(LogLevel logLevel) => true;

        public abstract IDisposable BeginScope<TState>(TState state);
    }
}
