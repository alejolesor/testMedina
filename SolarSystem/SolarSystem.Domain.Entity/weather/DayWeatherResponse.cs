using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarSystem.Domain.Entity.weather
{
	public class DayWeatherResponse
	{
		public int day { get; set; }
		public Weather weather { get; set; }

        public string weatherName { get; set; }


        public void setDay(int day)
		{
			this.day = day;
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
