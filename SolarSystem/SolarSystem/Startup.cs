using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SolarSystem.Aplication.Interface;
using SolarSystem.Aplication.Main;
using SolarSystem.Domain.Entity.weather;
using SolarSystem.Domain.Interface;
using SolarSystem.Infrastructure.Data;
using SolarSystem.Infrastucture.Repository;
using SolarSystem.Transversal.Common;
using System.Collections.Generic;

namespace SolarSystem
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SolarSystem", Version = "v1" });
            });

            services.AddSingleton<IConnectionFactory, ConnectionFactory>();

            services.AddScoped<IweatherForecasterRepository, WeatherForecasterImpRepository>();

            services.AddScoped<IWeatherForecaster, WeatherForecaster>();

            services.AddScoped<IPredictions, Predictions>();


            services.AddSingleton<ConnectionFactory>();


            services.Configure<Galaxy>(options=> Configuration.GetSection("ConfigPlanets").Bind(options));



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SolarSystem v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
