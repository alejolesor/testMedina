using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarSystem.Domain.Entity.weather
{
    public class DayWeather
    {
		private int id;
		private DateTime date;
		private Weather weather;
		private float areaTriangle;
		private long day;

		public DateTime getDate()
		{
			return date;
		}

		public void setDate(DateTime date)
		{
			this.date = date;
		}

		public Weather getWeather()
		{
			return weather;
		}

		public void setWeather(Weather weather)
		{
			this.weather = weather;
		}

		public float getAreaTriangle()
		{
			return areaTriangle;
		}

		public void setAreaTriangle(float areaTriangle)
		{
			this.areaTriangle = areaTriangle;
		}

		public int getId()
		{
			return id;
		}

		public void setId(int id)
		{
			this.id = id;
		}

		public long getDay()
		{
			return day;
		}

		public void setDay(long day)
		{
			this.day = day;
		}
	}
}
