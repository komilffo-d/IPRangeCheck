﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPRangeCheckConsole.Interfaces
{
    internal interface IFileWriter
    {
        public bool Write(IEnumerable<object> values);
    }
}
