using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPRangeCheckConsole.Misc
{
    public sealed class CLIOptionsSchema
    {
        [Required]
        public File JSON_FILE { get; set; }

        public Address JSON_ADDRESS { get; set; }

        [Required]
        public Time JSON_TIME { get; set; }

        public class File
        {
            [Required]
            public string Log { get; set; }

            [Required]
            public string Output { get; set; }
        }
        public class Address
        {
            [Required]
            public string Start { get; set; }

            [Required]
            public string Mask { get; set; }
        }

        public class Time
        {
            [Required]
            public string Start { get; set; }

            [Required]
            public string End { get; set; }
        }

    }



}
