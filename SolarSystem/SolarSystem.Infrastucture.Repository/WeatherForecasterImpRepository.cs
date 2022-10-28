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
            try
            {
                var query = "SELECT COUNT(ID) FROM [dbo].[dayweather] WHERE WEATHER = 'DRY'";
                using (var connection = _connectionFactory.GetConnection)
                {
                    var amountDryDays = connection.Query<int>(query);
                    return amountDryDays.AsList()[0];
                }
            }
            catch (System.Exception ex)
            {

                throw;
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
                    var dayweathers = connection.Execute(query, new Dayweather { Weather = dayWeather.getWeather().ToString(), Area_Triangle = dayWeather.getAreaTriangle(), Day = dayWeather.getDay()});
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
