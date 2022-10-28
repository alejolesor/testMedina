using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarSystem.Aplication.Interface;
using SolarSystem.Domain.Entity.weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolarSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecaster _weatherForecaster;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecaster weatherForecaster)
        {
            _logger = logger;
            _weatherForecaster = weatherForecaster;
        }

        [HttpGet]
        public async Task<DayWeatherResponse> GetAmountOfDryDays()
        {
            DayWeatherResponse weatherResponse = new DayWeatherResponse();
            weatherResponse.setDay(await _weatherForecaster.GetAmountOfDryDays());
            weatherResponse.weatherName = weatherResponse.weather.ToString();
            return weatherResponse;
        }

        [HttpPost]
        public ActionResult GenerateConditionsModel()
        {
            _weatherForecaster.GeneratePredictionWeather();

            return Ok();
        }
    }
}
