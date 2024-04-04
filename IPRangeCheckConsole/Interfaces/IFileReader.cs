using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPRangeCheckConsole.Interfaces
{
    internal interface IFileReader
    {
        public bool Read(string filePath);

        public IAsyncEnumerable<string> ReadAsync(string filePath);
    }
}
