using CommandLine;
using IPRangeCheckConsole.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace IPRangeCheckConsole.Misc.CommandLineState
{
    internal class CommandLineStrategy : ArgumentStrategy
    {

        public CommandLineStrategy(IFileWriter fileWriter, IFileReader fileReader) : base()
        {
            _fileWriter = fileWriter;
            _fileReader = fileReader;


        }
        private protected override async Task<CLIOptions> GetParameters(string[]? args = null)
        {

            Parser parser = new Parser(ps => ps.ParsingCulture = CultureInfo.DefaultThreadCurrentCulture);

            CLIOptions CLIOptions = await parser.ParseArguments<CLIOptions>(args).MapResult(async (CLIOptions opts) =>
            {
                return opts;

            }, async err =>
            {

                return null;
            });

            return CLIOptions;
        }

    }
}
