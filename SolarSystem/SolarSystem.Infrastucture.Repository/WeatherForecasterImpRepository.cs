using Dapper;
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
            var query = "SELECT COUNT(ID) FROM dayweather WHERE weather = 'DRY'";
            using (var connection = _connectionFactory.GetConnection)
            {
                var companies = await connection.QueryAsync<Dayweather>(query);
                return companies.AsList().Count;
            }
        }
    }
}
