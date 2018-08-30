using SunnyBeam.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunnyBeam.Csv
{
    public abstract class AutonamedCsvWriter<TRecord, TStats> : CsvWriter<TRecord, TStats>
    {
        protected abstract string GetFilenameFromStats(TStats stats);

        public TStats WriteToDirectory(IEnumerable<TRecord> records, string directory)
        {
            using (var tempFile = new DisposableFile())
            {
                var stats = this.Write(records, tempFile.Path);
                var destination = Path.Combine(directory, this.GetFilenameFromStats(stats));

                if (File.Exists(destination))
                {
                    File.Delete(destination);
                }

                File.Move(tempFile.Path, destination);

                return stats;
            }
        }
    }
}
