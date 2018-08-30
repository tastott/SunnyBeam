using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunnyBeam.Csv
{
    public class SunnyBeamRawCsvWriter : CsvWriter<Reading, SunnyBeamOutputStats>
    {
        protected override SunnyBeamOutputStats AggregateStats(SunnyBeamOutputStats stats, Reading record)
        {
            return stats.Aggregate(record);
        }

        protected override CsvClassMap<Reading> GetClassMap()
        {
            return new ReadingClassMap();
        }

        protected override SunnyBeamOutputStats GetInitialStats()
        {
            return SunnyBeamOutputStats.Initial();
        }
    }

    public class ReadingClassMap : CsvClassMap<Reading>
    {
        public ReadingClassMap()
        {
            this.Map(reading => reading.Date).TypeConverterOption("yyyy-MM-dd");
            this.Map(reading => reading.Time).TypeConverterOption(@"hh\:mm");
            this.Map(reading => reading.Kilowatts);
        }
    }
}
