using SolarSystem.Aplication.Interface;
using SolarSystem.Domain.Entity.weather;
using SolarSystem.Domain.Interface;
using System;
using System.Threading.Tasks;

namespace SolarSystem.Aplication.Main
{
    public class WeatherForecaster : IWeatherForecaster
    {
        private readonly IweatherForecasterRepository iweatherForecasterRepository;

        public WeatherForecaster(IweatherForecasterRepository weatherForecasterRepository)
        {
            iweatherForecasterRepository = weatherForecasterRepository;
        }

        public async Task<int> GetAmountOfDryDays()
        {
            return await iweatherForecasterRepository.GetAmountOfDryDaysAsync();
        }

        public int getAmountOfOptimalConditionDays()
        {
            throw new NotImplementedException();
        }

        public int getAmountOfRainyDays()
        {
            throw new NotImplementedException();
        }

        public DayWeather getDayWeatherByDate(DateTime dayWeatherDate)
        {
            throw new NotImplementedException();
        }

        public DayWeather getDayWeatherByDay(int day)
        {
            throw new NotImplementedException();
        }

        public DayWeather getDayWithPeakRainfall()
        {
            throw new NotImplementedException();
        }

        public DayWeather GeneratePredictionWeather(Galaxy galaxy)
        {
            throw new NotImplementedException();
        }
    }
}
