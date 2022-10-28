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

        [Route("/GetAmountOfDryDays")]
        [HttpGet]
        public async Task<DayWeatherResponse> GetAmountOfDryDays() //Cuántos períodos de sequía habrá
        {
            DayWeatherResponse weatherResponse = new DayWeatherResponse();
            weatherResponse.setDay(await _weatherForecaster.GetAmountOfDryDays());
            weatherResponse.weatherName = weatherResponse.weather.ToString();
            return weatherResponse;
        }

        [HttpPost]
        public ActionResult GenerateConditionsModel() //Generar un modelo de datos con las condiciones de todos los días hasta 10 años en adelante
        {
            _weatherForecaster.GeneratePredictionWeather();

            return Ok();
        }

        [Route("/countRaindays")]
        [HttpGet]
        public async Task<DayWeatherResponse> GetCountRainDays() //Cuántos períodos de lluvia habrá
        {
            DayWeatherResponse weatherResponse = new DayWeatherResponse();
            weatherResponse.setDay(await _weatherForecaster.getAmountOfRainyDays());
            weatherResponse.setWeather(Weather.RAINY);
            weatherResponse.weatherName = Weather.RAINY.ToString();
            return weatherResponse;
        }

        [Route("/rainypeak")]
        [HttpGet]
        public async Task<DayWeatherResponse> GetDayRainyPeak() //qué día será el pico máximo de lluvia?
        {
            DayWeather dayWeather = await _weatherForecaster.getDayWithPeakRainfall();
            DayWeatherResponse weatherResponse = new DayWeatherResponse();
            weatherResponse.setDay(dayWeather.getDay());
            weatherResponse.setWeather(dayWeather.getWeather());
            weatherResponse.weatherName = dayWeather.getWeather().ToString();
            return weatherResponse;
        }

        [Route("/getOptimalCondiionsDay")]
        [HttpGet]
        public ActionResult getAmountOfOptimalConditionDays() //Cuántos períodos de condiciones óptimas de presión y temperatura habrá?
        {
            int count = _weatherForecaster.getAmountOfOptimalConditionDays();
            DayWeatherCount dayWeatherCount = new DayWeatherCount();
            dayWeatherCount.setDayCount(count);
            dayWeatherCount.setWeather(Weather.OPTIMAL);

            return Ok(dayWeatherCount) ;
        }


        [Route("/getWeatherForDay")]
        [HttpGet]
        public ActionResult getWeatherForDay(int day) //devuelve en formato JSON la condición climática del día consultado
        {
            DayWeather dayWeather = _weatherForecaster.getDayWeatherByDay(day);
            DayWeatherResponse weatherResponse = new DayWeatherResponse();
            weatherResponse.setDay(dayWeather.getDay());
            weatherResponse.setWeather(dayWeather.getWeather());
            weatherResponse.weatherName=dayWeather.getWeather().ToString(); 

            return Ok(weatherResponse);
        }
    }
}
