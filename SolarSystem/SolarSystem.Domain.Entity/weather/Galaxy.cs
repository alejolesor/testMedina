using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarSystem.Domain.Entity.weather
{
	public class Galaxy
	{
		private List<Planet> planets;

		public List<Planet> getPlanets()
		{
			return planets;
		}

		public void setPlanets(List<Planet> planets)
		{
			this.planets = planets;
		}
	}
}
