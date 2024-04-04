using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPRangeGenerator.Base
{
    public abstract class BaseGenerator
    {
         public abstract  Random _random { get; init; }

        public BaseGenerator()
        {
            _random = new Random();
        }
    }
}
