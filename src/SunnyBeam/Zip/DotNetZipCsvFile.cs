using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SunnyBeam.Zip
{
    public class DotNetZipCsvFile : ICsvFile
    {
        private readonly ZipEntry entry;

        public DotNetZipCsvFile(ZipEntry entry)
        {
            this.entry = entry;
        }

        public string Name => this.entry.FileName;

        public Stream Open()
        {
            return this.entry.OpenReader();
        }
    }
}
