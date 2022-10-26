using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarSystem.Infrastructure.Data
{
    public class Dayweather
    {
        public int ID { get; set; }
        public string Weather { get; set; }
        public DateTime Date { get; set; }
        public float Area_Triangle { get; set; }
        public int Day { get; set; }
    }
}
