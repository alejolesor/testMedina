using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarSystem.Aplication.Main
{
    public class Calculates
    {
        private static float EPSILON = 0.01f;

        /**
         * Method that calculates the vector module using two points
         */

        public float calculateVectorModule(float ax, float ay, float bx, float by)
        {
            return (float)Math.Sqrt(Math.Pow(bx - ax, 2) + Math.Pow(by - ay, 2));
        }

        private float calculateSemiPerimeter(float a, float b, float c)
        {
            return (a + b + c) / 2;
        }

        /**
		 * Method that calculates the triangle area using semiperimeter and triangle sides
		 */

        public float calculateTriangleArea(float vectorA, float vectorB, float vectorC)
        {
            float semiperimeter = calculateSemiPerimeter(vectorA, vectorB, vectorC);
            return (float)Math.Sqrt(
                    semiperimeter * (semiperimeter - vectorA) * (semiperimeter - vectorB) * (semiperimeter - vectorC));
        }


        public float getEpsilon()
        {
            return EPSILON;
        }
    }
}
