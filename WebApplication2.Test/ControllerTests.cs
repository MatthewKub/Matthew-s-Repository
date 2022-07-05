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
    }
}