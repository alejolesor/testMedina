using Microsoft.Extensions.Options;
using SolarSystem.Aplication.Interface;
using SolarSystem.Domain.Entity.weather;
using SolarSystem.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace SolarSystem.Aplication.Main
{
    public class WeatherForecaster : IWeatherForecaster
    {
        private readonly IweatherForecasterRepository _iweatherForecasterRepository;
        private readonly IPredictions _ipredictions;
        private readonly Galaxy _galaxy;

        public WeatherForecaster(IweatherForecasterRepository weatherForecasterRepository, IPredictions predictions, IOptions<Galaxy> galaxy)
        {
            _iweatherForecasterRepository = weatherForecasterRepository;
            _ipredictions = predictions;
            _galaxy = galaxy.Value;

        }

        public async Task<int> GetAmountOfDryDays()
        {
            return await _iweatherForecasterRepository.GetAmountOfDryDaysAsync();
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

        public bool GeneratePredictionWeather()
        {
            Calendar calendar = new GregorianCalendar();
            int dayPrediction = 0;
            var galaxy = initConfigPlanets();
            DayWeather maxAreaDayWeather = null;

            while (dayPrediction < 365 * 10)
            {
                DayWeather currentDayWeather = _ipredictions.predictWeather(galaxy);
                currentDayWeather.setDate(DateTime.Now);
                currentDayWeather.setDay(dayPrediction);

                if (maxAreaDayWeather == null
                    || currentDayWeather.getAreaTriangle() > maxAreaDayWeather.getAreaTriangle())
                {
                    maxAreaDayWeather = currentDayWeather;
                }

                _iweatherForecasterRepository.saveDayWeather(currentDayWeather);
                dayPrediction++;
                calendar.AddDays(DateTime.Now, 1); //review this part maybe to fix

                updateGalaxy(galaxy);
   
            }

            maxAreaDayWeather.setWeather(Weather.RAINY_PEAK);
            _iweatherForecasterRepository.updateRainPeakDayWeather(maxAreaDayWeather);

            return true;
        }

        private Galaxy initConfigPlanets()
        {
            var galaxy = new Galaxy();
            galaxy.planets = _galaxy.planets;

            foreach (var planet in galaxy.planets)
            {
                updatePlanetCartesianCoordinates(planet);
            }
            return galaxy;


        }

        public void updatePlanetCartesianCoordinates(Planet planet)
        {
            planet.setxCoordinate((float)Math.Cos(ConvertDegreesToRadians(planet.getAngle())) * planet.getDistanceToSunInKm());
            planet.setyCoordinate((float)Math.Sin(ConvertDegreesToRadians(planet.getAngle())) * planet.getDistanceToSunInKm());
        }

        public static double ConvertDegreesToRadians(double degrees)
        {
            double radians = (Math.PI / 180) * degrees;
            return (radians);
        }

        private void updateGalaxy(Galaxy galaxy)
        {
            foreach (var planet in galaxy.getPlanets())
            {
                float currentAngle = planet.getAngle() + planet.getDailyAngleVariation();
                if (currentAngle < 0)
                {
                    currentAngle = 360 + currentAngle;
                }

                planet.setAngle(currentAngle % 360);
                updatePlanetCartesianCoordinates(planet);
            }



        }


    }
}
