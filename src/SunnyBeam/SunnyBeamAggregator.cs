using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunnyBeam
{
    public static class SunnyBeamAggregator
    {
        public static IEnumerable<SolarDay> ToDaily(IEnumerable<Reading> readings)
        {
            return readings.GroupBy(reading => reading.Date)
                .Select(day => new SolarDay(day.Key, day.Sum(reading => reading.Kilowatts) / 6));
        }
    }
}
