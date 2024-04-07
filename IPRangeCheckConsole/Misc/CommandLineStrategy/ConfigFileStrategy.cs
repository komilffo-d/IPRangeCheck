using CommandLine;
using FluentValidation;
using IPRangeCheckConsole.Interfaces;
using Microsoft.Extensions.Configuration;
using Serilog;
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

            CLIOptions? CLIOptions = parser.ParseArguments<CLIOptions>(args).MapResult(opts => opts, err => null);

            if (!_configuration.GetChildren().Any())
            {
                Log.Warning("Попытка получить данные из конфигурационного файла не успешна!");
                return null!;
            }



            CLIOptionsSchema.File fileSection = new CLIOptionsSchema.File();
            _configuration.GetSection(nameof(CLIOptionsSchema.File)).Bind(fileSection);

            CLIOptionsSchema.Address addressSection = new CLIOptionsSchema.Address();
            _configuration.GetSection(nameof(CLIOptionsSchema.Address))?.Bind(addressSection);

            CLIOptionsSchema.Time timeSection = new CLIOptionsSchema.Time();
            _configuration.GetSection(nameof(CLIOptionsSchema.Time))?.Bind(timeSection);
            Log.Warning("Попытка получить данные из конфигурационного файла успешна!");
            return new CLIOptions()
            {
                FileLog = CLIOptions.FileLog ?? fileSection.Log,
                FileOutput = CLIOptions.FileOutput ?? fileSection.Output,
                AddressStart = CLIOptions.AddressStart ?? addressSection.Start,
                AddressMask = CLIOptions.AddressMask ?? addressSection.Mask,
                TimeStart = CLIOptions.TimeStart ?? timeSection.Start,
                TimeEnd = CLIOptions.TimeEnd ?? timeSection.End
            };


        }


    }


}
