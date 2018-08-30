using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunnyBeam.Csv
{
    public class SunnyBeamOutputStats
    {
        public readonly DateTime From;
        public readonly DateTime To;
        public readonly int ReadingCount;

        private SunnyBeamOutputStats(DateTime from, DateTime to, int readingCount)
        {
            this.From = from;
            this.To = to;
            this.ReadingCount = readingCount;
        }

        public static SunnyBeamOutputStats Initial()
        {
            return new SunnyBeamOutputStats(DateTime.MaxValue, DateTime.MinValue, 0);
        }

        public SunnyBeamOutputStats Aggregate(IDatedRecord reading)
        {
            var from = reading.Date < this.From ? reading.Date : this.From;
            var to = reading.Date > this.To ? reading.Date : this.To;

            return new SunnyBeamOutputStats(from, to, this.ReadingCount + 1);
        }

        public override string ToString()
        {
            return $"{this.From:yyyy-MM-dd} to {this.To:yyyy-MM-dd} | {this.ReadingCount} readings";
        }
    }
}
