using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunnyBeam
{
    public class SolarDay : IDatedRecord
    {
        private readonly DateTime date;
        private readonly decimal kilowattHours;

        public SolarDay(DateTime date, decimal kilowattHours)
        {
            this.date = date;
            this.kilowattHours = kilowattHours;
        }

        public DateTime Date => this.date;
        public decimal KilowattHours => this.kilowattHours;
    }
}
