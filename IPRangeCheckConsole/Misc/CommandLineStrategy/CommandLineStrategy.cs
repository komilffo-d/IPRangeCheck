using CommandLine;
using FluentValidation;
using FluentValidation.Results;
using IPRangeCheckConsole.Interfaces;
using System.Globalization;

namespace IPRangeCheckConsole.Misc.CommandLineState
{
    internal class CommandLineStrategy : ArgumentStrategy
    {

        public CommandLineStrategy(IFileWriter fileWriter, IFileReader fileReader, IValidator<CLIOptions> validator) : base()
        {
            _fileWriter = fileWriter;
            _fileReader = fileReader;
            _validator = validator;

        }
        private protected override async Task<CLIOptions> GetParameters(string[]? args = null)
        {

            Parser parser = new Parser(ps =>
            {
                ps.ParsingCulture = CultureInfo.DefaultThreadCurrentCulture;
                ps.IgnoreUnknownArguments = true;
            });

            CLIOptions? CLIOptions = await parser.ParseArguments<CLIOptions>(args).MapResult(async (CLIOptions opts) =>
            {
                ValidationResult result = await _validator.ValidateAsync(opts);
                if (result.IsValid)
                    return opts;
                return null;
            }, err =>
            {
                return null;
            });


            return CLIOptions!;
        }

    }
}
