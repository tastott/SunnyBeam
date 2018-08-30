using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunnyBeam
{
    public class Reading : IDatedRecord
    {
        private readonly DateTimeOffset time;
        private readonly decimal kilowatts;

        public Reading(DateTimeOffset time, decimal kilowatts)
        {
            this.time = time;
            this.kilowatts = kilowatts;
        }

        public DateTime Date => this.time.Date;
        public TimeSpan Time => this.time.TimeOfDay;

        public decimal Kilowatts => this.kilowatts;
    }
}
