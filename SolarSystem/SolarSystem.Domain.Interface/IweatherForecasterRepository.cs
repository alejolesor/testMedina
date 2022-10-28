using SolarSystem.Domain.Entity.weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarSystem.Domain.Interface
{
    public interface IweatherForecasterRepository
    {
        Task<int> GetAmountOfDryDaysAsync();

        Task saveDayWeatherAsync(DayWeather dayWeather);  //missing implement

        Task updateRainPeakDayWeatherAsync(DayWeather maxAreaDayWeather); //missing implement
    }
}
