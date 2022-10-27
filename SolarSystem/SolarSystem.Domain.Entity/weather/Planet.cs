using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarSystem.Domain.Entity.weather
{
	public class Planet
	{
        public string name { get; set; }
		public float dailyAngleVariation { get; set; }
		public float distanceToSunInKm { get; set; }
		public float angle { get; set; }
		public float xCoordinate { get; set; }
		public float yCoordinate { get; set; }

		public float getDistanceToSunInKm()
		{
			return distanceToSunInKm;
		}

		public void setDistanceToSunInKm(float distanceToSunInKm)
		{
			this.distanceToSunInKm = distanceToSunInKm;
		}

		public float getAngle()
		{
			return angle;
		}

		public void setAngle(float angle)
		{
			this.angle = angle;
			this.xCoordinate = 0;
			this.yCoordinate = 0;
		}

		public float getxCoordinate()
		{
			return xCoordinate;
		}

		public void setxCoordinate(float xCoordinate)
		{
			this.xCoordinate = xCoordinate;
		}

		public float getyCoordinate()
		{
			return yCoordinate;
		}

		public void setyCoordinate(float yCoordinate)
		{
			this.yCoordinate = yCoordinate;
		}

		public String getName()
		{
			return name;
		}

		public void setName(String name)
		{
			this.name = name;
		}

		public float getDailyAngleVariation()
		{
			return dailyAngleVariation;
		}

		public void setDailyAngleVariation(float dailyAngleVariation)
		{
			this.dailyAngleVariation = dailyAngleVariation;
		}
	}
}
