using CommandLine;
using FluentValidation;
using IPRangeCheckConsole.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace IPRangeCheckConsole.Misc.CommandLineState
{
    internal class ConfigFileStrategy : ArgumentStrategy
    {
        private readonly IConfiguration _configuration;
        public ConfigFileStrategy(IFileWriter fileWriter, IFileReader fileReader, IConfiguration configuration, IValidator<CLIOptions> validator) : base()
        {
            _fileWriter = fileWriter;
            _fileReader = fileReader;
            _configuration = configuration;
            _validator = validator;
        }

        private protected override async Task<CLIOptions> GetParameters(string[]? args = null)
        {
            Parser parser = new Parser(ps =>
            {
                ps.ParsingCulture = CultureInfo.DefaultThreadCurrentCulture;
                ps.IgnoreUnknownArguments = true;
            });

            CLIOptions? CLIOptions =  parser.ParseArguments<CLIOptions>(args).MapResult(opts => opts, err => null);
            
            try
            {

                File fileConfiguration = new File();
                _configuration?.GetSection("file")?.Bind(fileConfiguration);
                Address addressConfiguration = new Address();
                _configuration?.GetSection("address")?.Bind(addressConfiguration);
                Time timeConfiguration = new Time();
                _configuration?.GetSection("time")?.Bind(timeConfiguration);

                return new CLIOptions()
                {
                    FileLog = CLIOptions.FileLog ?? fileConfiguration.Log,
                    FileOutput = CLIOptions.FileOutput ?? fileConfiguration.Output,
                    AddressStart = CLIOptions.AddressStart ?? addressConfiguration.Start,
                    AddressMask = CLIOptions.AddressMask ?? addressConfiguration.Mask,
                    TimeStart = CLIOptions.TimeStart ?? timeConfiguration.Start,
                    TimeEnd = CLIOptions.TimeEnd ?? timeConfiguration.End
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
        public DateOnly? Start { get; set; }
        public DateOnly? End { get; set; }
    }
}
