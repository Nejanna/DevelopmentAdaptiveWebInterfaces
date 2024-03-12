using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace projectWebApi.Controllers
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

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        public IActionResult Post([FromBody] WeatherForecast forecast)
        {
            try
            {
                if (forecast == null)
                {
                    return BadRequest("Помилка: Неправильний прогноз погоди.");
                }

                string fileName = "weather_forecast.json";
                if (!System.IO.File.Exists(fileName))
                {
                    return StatusCode(500, "Файл прогнозів погоди не існує.");
                }
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                string json = JsonSerializer.Serialize(forecast, options);
                using (StreamWriter sw = new StreamWriter(fileName, true))
                {
                    sw.WriteLine(json);
                }
                return Ok($"Прогноз погоди збережено у файлі: {fileName}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Помилка під час збереження прогнозу погоди: {ex.Message}");
            }
        }
    }
}