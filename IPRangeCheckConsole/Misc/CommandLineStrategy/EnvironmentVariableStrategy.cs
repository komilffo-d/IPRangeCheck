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
        public EnvironmentVariableStrategy(IFileWriter fileWriter, IFileReader fileReader) : base()
        {
            _fileWriter = fileWriter;
            _fileReader = fileReader;
        }

        private protected override async Task<CLIOptions> GetParameters(string[]? args = null)
        {
            Dictionary<string, string> envVariable = Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Process).Cast<DictionaryEntry>().ToDictionary(entry => entry.Key.ToString(), entry => entry.Value.ToString());

            bool IsNotAllRequiredEnvVar = envVariable.Any(envVar => !REQUIRED_VARIABLES.ContainsKey(envVar.Key) || REQUIRED_VARIABLES[envVar.Key] == true);

/*            if (IsNotAllRequiredEnvVar)
                return null;*/

            var nessesaryEnvVar = envVariable.Where(envVar => REQUIRED_VARIABLES.ContainsKey(envVar.Key)).ToDictionary();


            var key = IPAddressRange.TryParse($"{IPAddress.None}/255.255.255.192", out IPAddressRange maskIPAddress1);
            



            return new CLIOptions()
            {
                FileLog = nessesaryEnvVar["FILE_LOG"],
                FileOutput = nessesaryEnvVar["FILE_OUTPUT"],
                AddressStart = IPAddress.TryParse(nessesaryEnvVar["ADDRESS_START"], out IPAddress startIPAddress) ? startIPAddress.ToString() : "0.0.0.0",
                AddressMask = IPAddress.TryParse(nessesaryEnvVar["ADDRESS_MASK"], out IPAddress maskIPAddress) ? maskIPAddress.ToString() : "0.0.0.0",
                TimeStart = DateTime.TryParseExact(nessesaryEnvVar["TIME_START"], "dd.M.yyyy", null, DateTimeStyles.None, out DateTime tempMinDateTime) ? tempMinDateTime : new DateTime(1900, 1, 1),
                TimeEnd = DateTime.TryParseExact(nessesaryEnvVar["TIME_END"], "dd.M.yyyy", null, DateTimeStyles.None, out DateTime tempMaxDateTime) ? tempMaxDateTime : new DateTime(2099, 12, 31),
            };
        }
    }
}
