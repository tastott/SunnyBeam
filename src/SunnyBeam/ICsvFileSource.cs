﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunnyBeam
{
    public interface ICsvFileSource
    {
        IEnumerable<ICsvFile> Enumerate();
    }
}
