using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunnyBeam.Zip
{
    public class DotNetZipCsvFileSource : ICsvFileSource
    {
        private readonly Func<Stream> open;

        public DotNetZipCsvFileSource(string filepath)
            : this(() => File.OpenRead(filepath))
        {
        }

        public DotNetZipCsvFileSource(Func<Stream> open)
        {
            this.open = open;
        }

        public IEnumerable<ICsvFile> Enumerate()
        {
            using (var zipStream = this.open())
            using (var zipFile = ZipFile.Read(zipStream))
            {
                foreach (var entry in zipFile.Entries)
                {
                    yield return new DotNetZipCsvFile(entry);
                }
            }
        }
    }
}
