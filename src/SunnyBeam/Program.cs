using CsvHelper;
using SunnyBeam.Csv;
using SunnyBeam.Zip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunnyBeam
{
    class Program
    {
        static void Main(string[] args)
        {
            var source = new DotNetZipCsvFileSource(args[0]);
            var reader = new SunnyBeamCsvReader(source);
            var daily = SunnyBeamAggregator.ToDaily(reader.ReadAll())
                .OrderBy(record => record.Date);

            var writer = new SunnyBeamDailyCsvWriter();
            var stats = writer.WriteToDirectory(daily, args[1]);


            Console.WriteLine(stats);

            Console.ReadKey();
        }
    }
}
