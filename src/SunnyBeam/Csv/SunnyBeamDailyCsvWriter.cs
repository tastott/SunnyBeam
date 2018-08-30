using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunnyBeam.Csv
{
    public class SunnyBeamDailyCsvWriter : AutonamedCsvWriter<SolarDay, SunnyBeamOutputStats>
    {
        protected override SunnyBeamOutputStats AggregateStats(SunnyBeamOutputStats stats, SolarDay record)
        {
            return stats.Aggregate(record);
        }

        protected override CsvClassMap<SolarDay> GetClassMap()
        {
            return new SolarDayClassMap();
        }

        protected override string GetFilenameFromStats(SunnyBeamOutputStats stats)
        {
            return $"Sunny Beam Daily {stats.From:yyyy-MM-dd} to {stats.To:yyyy-MM-dd}.csv";
        }

        protected override SunnyBeamOutputStats GetInitialStats()
        {
            return SunnyBeamOutputStats.Initial();
        }

        public class SolarDayClassMap : CsvClassMap<SolarDay>
        {
            public SolarDayClassMap()
            {
                this.Map(reading => reading.Date).TypeConverterOption("yyyy-MM-dd");
                this.Map(reading => reading.KilowattHours);
            }
        }
    }
}
