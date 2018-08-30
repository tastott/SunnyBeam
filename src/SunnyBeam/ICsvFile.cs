using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunnyBeam
{
    public interface ICsvFile
    {
        string Name { get; }
        Stream Open();
    }
}
