using SolarSystem.Domain.Entity.weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarSystem.Aplication.Interface
{
    public interface IWeatherForecaster
    {
		bool GeneratePredictionWeather();

		Task<DayWeather> getDayWithPeakRainfall();


		Task<int> getAmountOfRainyDays();

		Task<int> GetAmountOfDryDays();

		int getAmountOfOptimalConditionDays();

		DayWeather getDayWeatherByDay(int day);

	}
}
