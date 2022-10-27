using SolarSystem.Aplication.Interface;
using SolarSystem.Domain.Entity.weather;
using SolarSystem.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarSystem.Aplication.Main
{
    public class Predictions : IPredictions
    {
        private readonly IweatherForecasterRepository _iweatherForecasterRepository;
        public Predictions(IweatherForecasterRepository iweatherForecasterRepository)
        {
            _iweatherForecasterRepository = iweatherForecasterRepository;
        }

        public DayWeather predictWeather(Galaxy galaxy)
        {
            //refactor here
            Calculates calculates = new Calculates();

            Planet firstPlanet = galaxy.getPlanets().Where(x => x.name == "FERENGI").First();
            Planet secondPlanet = galaxy.getPlanets().Where(x => x.name == "BETASOIDE").First();
            Planet thirdPlanet = galaxy.getPlanets().Where(x => x.name == "VULCANO").First();

            DayWeather dayWeather = new DayWeather();

            if (arePlanetsAligned(firstPlanet, secondPlanet, thirdPlanet))
            {
                if (arePlanetsAlignedWithTheSun(firstPlanet, secondPlanet))
                {
                    dayWeather.setWeather(Weather.DRY);
                }
                else
                {
                    dayWeather.setWeather(Weather.OPTIMAL);
                }
            }
            else
            {
                float ab = calculates.calculateVectorModule(firstPlanet.getxCoordinate(), firstPlanet.getyCoordinate(),
                        secondPlanet.getxCoordinate(), secondPlanet.getyCoordinate());
                float bc = calculates.calculateVectorModule(secondPlanet.getxCoordinate(), secondPlanet.getyCoordinate(),
                        thirdPlanet.getxCoordinate(), thirdPlanet.getyCoordinate());
                float ac = calculates.calculateVectorModule(firstPlanet.getxCoordinate(), firstPlanet.getyCoordinate(),
                        thirdPlanet.getxCoordinate(), thirdPlanet.getyCoordinate());
                float areaABC = calculates.calculateTriangleArea(ab, bc, ac);

                if (triangleFormedByPlanetsContainsTheSun(firstPlanet, secondPlanet, thirdPlanet, ab, bc,
                        ac, areaABC))
                {
                    dayWeather.setWeather(Weather.RAINY);
                    dayWeather.setAreaTriangle(areaABC);
                }
            }



            return dayWeather;
        }


        public bool arePlanetsAligned(Planet firstPlanet, Planet secondPlanet, Planet thirdPlanet)
        {
            Calculates calculates = new Calculates();

            float distanceAB = calculates.calculateVectorModule(firstPlanet.getxCoordinate(), firstPlanet.getyCoordinate(),
                    secondPlanet.getxCoordinate(), secondPlanet.getyCoordinate());
            float distanceBC = calculates.calculateVectorModule(secondPlanet.getxCoordinate(),
                    secondPlanet.getyCoordinate(), thirdPlanet.getxCoordinate(), thirdPlanet.getyCoordinate());
            float distanceAC = calculates.calculateVectorModule(firstPlanet.getxCoordinate(), firstPlanet.getyCoordinate(),
                    thirdPlanet.getxCoordinate(), thirdPlanet.getyCoordinate());
            return Math.Abs(distanceAB + distanceBC - distanceAC) <= calculates.getEpsilon();
        }

        public bool arePlanetsAlignedWithTheSun(Planet firstPlanet, Planet secondPlanet)
        {
            Calculates calculates = new Calculates();
            float slope = (firstPlanet.getyCoordinate() - secondPlanet.getyCoordinate())
                    / (firstPlanet.getxCoordinate() - secondPlanet.getxCoordinate());
            float yIntercept = firstPlanet.getyCoordinate() - slope * firstPlanet.getxCoordinate();
            return Math.Abs(yIntercept) <= calculates.getEpsilon();
        }

        public bool triangleFormedByPlanetsContainsTheSun(Planet firstPlanet, Planet secondPlanet, Planet thirdPlanet,
        float ab, float bc, float ac, float areaABC)
        {
            Calculates calculates = new Calculates();
            // Point 0,0 is the Sun. Calculating vectors from each planet to Sun
            float va = calculates.calculateVectorModule(firstPlanet.getxCoordinate(), firstPlanet.getyCoordinate(), 0, 0);
            float vb = calculates.calculateVectorModule(secondPlanet.getxCoordinate(), secondPlanet.getyCoordinate(), 0,
                    0);
            float cs = calculates.calculateVectorModule(thirdPlanet.getxCoordinate(), thirdPlanet.getyCoordinate(), 0, 0);

            // Calculating area of triangles formed by Sun and planets
            float areaFSS = calculates.calculateTriangleArea(va, vb, ab);
            float areaFTS = calculates.calculateTriangleArea(va, cs, ac);
            float areaSTS = calculates.calculateTriangleArea(vb, cs, bc);

            // If total area is equals to the three new areas, sun is contained in the
            // triangle
            float areaSUM = areaFSS + areaFTS + areaSTS;

            return Math.Abs(areaSUM - areaABC) <= calculates.getEpsilon();
        }
    }
}
