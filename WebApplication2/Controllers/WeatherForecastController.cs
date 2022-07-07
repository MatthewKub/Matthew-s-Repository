using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [Route("/add")]
        [HttpGet]
        public int Add(int x, int y)
        {
              _logger.LogInformation("x = " + x + " y = " + y);
              int z = x + y;
              return z; 
        }

        /// <summary>
        /// Returns the first element of array
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        [Route("/FirstElement")]
        [HttpGet]
        public int FirstElement([FromQuery] int[] a)
        {
            _logger.LogInformation("Received an array having Length=" + a.Length);
            return a[0];
        }

        [Route("/product")]
        [HttpGet]
        public int Product(int x, int y, int z)
        {
            _logger.LogInformation("x = " + x + " y = " + y + " z = " + z);
            int r = x * y *z;
            return r;
        }

       
        
    }
}