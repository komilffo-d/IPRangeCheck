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
        public File file { get; set; }

        public Address address { get; set; }

        [Required]
        public Time time { get; set; }

        public class File
        {
            [Required]
            public string log { get; set; }

            [Required]
            public string output { get; set; }
        }
        public class Address
        {
            [Required]
            public string start { get; set; }

            [Required]
            public string mask { get; set; }
        }

        public class Time
        {
            [Required]
            public string start { get; set; }

            [Required]
            public string end { get; set; }
        }

    }



}
