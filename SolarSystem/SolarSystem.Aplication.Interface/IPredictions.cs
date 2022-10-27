using SolarSystem.Domain.Entity.weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarSystem.Aplication.Interface
{
    public interface IPredictions
    {
        DayWeather predictWeather(Galaxy galaxy);
    }
}
