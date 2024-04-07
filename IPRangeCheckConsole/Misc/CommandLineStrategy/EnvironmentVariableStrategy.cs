using CommandLine;
using FluentValidation;
using IPRangeCheckConsole.Interfaces;
using IPRangeGenerator.Services;
using NetTools;
using System.Collections;
using System.Globalization;
using System.Net;

namespace IPRangeCheckConsole.Misc.CommandLineState
{
    internal class EnvironmentVariableStrategy : ArgumentStrategy
    {
        private static readonly Dictionary<string, bool> REQUIRED_VARIABLES = new Dictionary<string, bool>() {
            { "FILE_LOG", true },
            { "FILE_OUTPUT",true },
            {"ADDRESS_START",false },
            {"ADDRESS_MASK",false },
            {"TIME_START",true },
            {"TIME_END",true }
        };
        public EnvironmentVariableStrategy(IFileWriter fileWriter, IFileReader fileReader, IValidator<CLIOptions> validator) : base()
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

            CLIOptions? CLIOptions = parser.ParseArguments<CLIOptions>(args).MapResult(opts => opts, err => null);

            Dictionary<string, string> envVariable = Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Process).Cast<DictionaryEntry>().ToDictionary(entry => entry.Key.ToString(), entry => entry.Value.ToString());

            bool IsNotAllRequiredEnvVar = envVariable.Any(envVar => !REQUIRED_VARIABLES.ContainsKey(envVar.Key) || REQUIRED_VARIABLES[envVar.Key] == true);

/*            if (IsNotAllRequiredEnvVar)
                return null;*/

            var nessesaryEnvVar = envVariable.Where(envVar => REQUIRED_VARIABLES.ContainsKey(envVar.Key)).ToDictionary();


            var key = IPAddressRange.TryParse($"{IPAddress.None}/255.255.255.192", out IPAddressRange maskIPAddress1);

            nessesaryEnvVar.TryGetValue("FILE_LOG", out string envFileLog);
            nessesaryEnvVar.TryGetValue("FILE_OUTPUT", out string envFileOutput);
            nessesaryEnvVar.TryGetValue("ADDRESS_START", out string envAddressStart);
            nessesaryEnvVar.TryGetValue("ADDRESS_MASK", out string envAddressMask);
            nessesaryEnvVar.TryGetValue("TIME_START", out string envTimeStart);
            nessesaryEnvVar.TryGetValue("TIME_END", out string envTimeEnd);


            return new CLIOptions()
            {
                FileLog = CLIOptions.FileLog ?? envFileLog,
                FileOutput = CLIOptions.FileOutput ?? envFileOutput,
                AddressStart = CLIOptions.AddressStart ?? (IPAddress.TryParse(envAddressStart, out IPAddress startIPAddress) ? startIPAddress.ToString() : null!),
                AddressMask = CLIOptions.AddressMask ?? (IPAddress.TryParse(envAddressMask, out IPAddress maskIPAddress) ? maskIPAddress.ToString() : null!),
                TimeStart = CLIOptions.TimeStart ?? (DateOnly.TryParseExact(envTimeStart, "dd.M.yyyy", null, DateTimeStyles.None, out DateOnly tempMinDateTime) ? tempMinDateTime: null!),
                TimeEnd = CLIOptions.TimeEnd ?? (DateOnly.TryParseExact(envTimeEnd, "dd.M.yyyy", null, DateTimeStyles.None, out DateOnly tempMaxDateTime) ? tempMaxDateTime :null!),
            };

        }
    }
}
