using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarSystem.Domain.Entity.weather
{
	public class DayWeatherCount
	{
		private int dayCount;
		private Weather weather;

		public int getDayCount()
		{
			return dayCount;
		}

		public void setDayCount(int dayCount)
		{
			this.dayCount = dayCount;
		}

		public Weather getWeather()
		{
			return weather;
		}

		public void setWeather(Weather weather)
		{
			this.weather = weather;
		}

	}
}
