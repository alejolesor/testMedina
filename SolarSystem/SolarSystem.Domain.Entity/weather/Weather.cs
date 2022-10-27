using Google.Apis.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SolarSystem.Domain.Entity.weather
{
    public enum Weather
    {
        [StringValue("DRY")]
        DRY = 0,
        RAINY,
        RAINY_PEAK,
        OPTIMAL
    }
}
