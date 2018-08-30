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
    public abstract class CsvWriter<TRecord, TStats>
    {
        protected abstract CsvClassMap<TRecord> GetClassMap();
        protected abstract TStats GetInitialStats();
        protected abstract TStats AggregateStats(TStats stats, TRecord record);

        public TStats Write(IEnumerable<TRecord> records, string outputFile)
        {
            using (var stream = File.Create(outputFile))
            {
                return this.Write(records, stream);
            }
        }

        public TStats Write(IEnumerable<TRecord> records, Stream outputStream)
        {
            using (var textWriter = new StreamWriter(outputStream, Encoding.UTF8, 1024 * 64, true))
            using (var csvWriter = new CsvWriter(textWriter))
            {
                csvWriter.Configuration.RegisterClassMap(this.GetClassMap());
                csvWriter.WriteHeader<TRecord>();

                var stats = this.GetInitialStats();

                foreach (var record in records)
                {
                    csvWriter.WriteRecord(record);
                    stats = this.AggregateStats(stats, record);
                }

                return stats;
            }
        }
    }
}
