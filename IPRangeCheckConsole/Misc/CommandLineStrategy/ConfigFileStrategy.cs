using IPRangeCheckConsole.Interfaces;
using Microsoft.Extensions.Configuration;

namespace IPRangeCheckConsole.Misc.CommandLineState
{
    internal class ConfigFileStrategy : ArgumentStrategy
    {
        private readonly IConfiguration _configuration;
        public ConfigFileStrategy(IFileWriter fileWriter, IFileReader fileReader, IConfiguration configuration) : base()
        {
            _fileWriter = fileWriter;
            _fileReader = fileReader;
            _configuration = configuration;
        }

        private protected override async Task<CLIOptions> GetParameters(string[]? args = null)
        {
            try
            {

                File fileConfiguration = new File();
                _configuration?.GetRequiredSection("file")?.Bind(fileConfiguration);
                Address addressConfiguration = new Address();
                _configuration?.GetSection("address")?.Bind(addressConfiguration);
                Time timeConfiguration = new Time();
                _configuration?.GetRequiredSection("time")?.Bind(timeConfiguration);

                return new CLIOptions()
                {
                    FileLog = fileConfiguration.Log,
                    FileOutput = fileConfiguration.Output,
                    AddressStart=addressConfiguration.Start,
                    AddressMask=addressConfiguration.Mask,
                    TimeStart=timeConfiguration.Start,
                    TimeEnd=timeConfiguration.End
                };
            }
            catch
            {
                return null;
            }
        }


    }

    file sealed class File
    {
        public string Log { get; set; }
        public string Output { get; set; }
    }

    file sealed class Address
    {
        public string Start { get; set; }
        public string Mask { get; set; }
    }

    file sealed class Time
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
