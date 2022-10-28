using Dapper;
using SolarSystem.Domain.Entity.weather;
using SolarSystem.Domain.Interface;
using SolarSystem.Infrastructure.Data;
using SolarSystem.Transversal.Common;
using System.Threading.Tasks;

namespace SolarSystem.Infrastucture.Repository
{
    public class WeatherForecasterImpRepository : IweatherForecasterRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public WeatherForecasterImpRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<int> GetAmountOfDryDaysAsync()
        {

            var query = "SELECT COUNT(ID) FROM [dbo].[dayweather] WHERE WEATHER = 'DRY'";
            using (var connection = _connectionFactory.GetConnection)
            {
                var amountDryDays = await connection.QueryAsync<int>(query);
                return amountDryDays.AsList()[0];
            }

        }

        public int getAmountOfOptimalConditionDays()
        {
            var query = "SELECT COUNT(ID) FROM dayweather WHERE weather = 'OPTIMAL'";
            using (var connection = _connectionFactory.GetConnection)
            {
                var amountOfRainyDays = connection.Query<int>(query);
                return amountOfRainyDays.AsList()[0];
            }
        }

        public async Task<int> getAmountOfRainyDays()
        {
            var query = "SELECT COUNT(ID) FROM [dbo].[dayweather] WHERE WEATHER = 'RAINY' OR WEATHER = 'RAINY_PEAK'";
            using (var connection = _connectionFactory.GetConnection)
            {
                var amountOfRainyDays = await connection.QueryAsync<int>(query);
                return amountOfRainyDays.AsList()[0];
            }
        }

        public DayWeather getDayWeatherByDay(int dayWeatherDay)
        {
            var query = "SELECT ID, WEATHER, AREA_TRIANGLE as areaTriangle, DAY FROM [dbo].[dayweather] WHERE DAY = @DAY";
            using (var connection = _connectionFactory.GetConnection)
            {
                var amountOfRainyDays = connection.Query<DayWeather>(query, new { day = dayWeatherDay });
                return amountOfRainyDays.AsList()[0];
            }

        }

        public async Task<DayWeather> getDayWithRainPeakDayWeather()
        {
            DayWeather dayWeather = new DayWeather();
            dayWeather.setDay(0);
            dayWeather.setWeather(Weather.WITHOUTPREDICTIONS);


            var query = "SELECT ID, WEATHER, DATE, AREA_TRIANGLE as areaTriangle, DAY FROM [dbo].[dayweather] WHERE WEATHER = 'RAINY_PEAK'";
            using (var connection = _connectionFactory.GetConnection)
            {
                var amountOfRainyDays = await connection.QueryAsync<DayWeather>(query);

                if (amountOfRainyDays.AsList().Count > 0)
                {
                    return amountOfRainyDays.AsList()[0];
                }
                return dayWeather; //refactor this part
            }
        }

        public async Task saveDayWeatherAsync(DayWeather dayWeather)
        {
            try
            {
                var weather = dayWeather.getWeather();
                var query = $"INSERT INTO [dbo].[dayweather] (WEATHER,AREA_TRIANGLE,DAY) VALUES ( (@WEATHER),(@AREA_TRIANGLE),(@DAY) )";
                using (var connection = _connectionFactory.GetConnection)
                {
                    var dayweathers = connection.Execute(query, new Dayweather { Weather = dayWeather.getWeather().ToString(), Area_Triangle = dayWeather.getAreaTriangle(), Day = dayWeather.getDay() });
                    return;
                }
            }
            catch (System.Exception ex)
            {
                throw;
            }

        }

        public async Task updateRainPeakDayWeatherAsync(DayWeather maxAreaDayWeather)
        {
            var query = $"UPDATE [dbo].[dayweather] SET WEATHER = {maxAreaDayWeather.getWeather()} WHERE ID = {maxAreaDayWeather.getId()}";
            using (var connection = _connectionFactory.GetConnection)
            {
                var rainPeak = await connection.QueryAsync<Dayweather>(query);
                return;
            }
        }


    }
}
