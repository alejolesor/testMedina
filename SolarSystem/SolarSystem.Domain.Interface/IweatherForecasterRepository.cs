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

        void saveDayWeather(DayWeather dayWeather);  //missing implement

        void updateRainPeakDayWeather(DayWeather maxAreaDayWeather); //missing implement
    }
}
