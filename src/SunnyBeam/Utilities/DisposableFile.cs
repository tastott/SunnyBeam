using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunnyBeam.Utilities
{
    public class DisposableFile : IDisposable
    {
        public readonly string Path;

        public DisposableFile()
        {
            this.Path = System.IO.Path.GetTempFileName();
            File.Delete(this.Path);
        }

        public void Dispose()
        {
            if (File.Exists(this.Path))
            {
                File.Delete(this.Path);
            }
        }
    }
}
