using IPRangeCheckConsole.Interfaces;

namespace IPRangeCheckConsole.Misc.CommandLineState
{
    internal class EnvironmentVariableState : ArgumentState
    {
        public EnvironmentVariableState(IFileWriter fileWriter, IFileReader fileReader) : base()
        {
            _fileWriter = fileWriter;
            _fileReader = fileReader;
        }

        private protected override Task<CLIOptions> GetParameters(string[]? args = null)
        {
            string? AddressStart = Environment.GetEnvironmentVariable("ADDRESS_START");
            string? AddressMask = Environment.GetEnvironmentVariable("ADDRESS_MASK");
            return null!;
        }
    }
}
