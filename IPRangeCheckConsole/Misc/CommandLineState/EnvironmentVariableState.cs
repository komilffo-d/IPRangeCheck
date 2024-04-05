namespace IPRangeCheckConsole.Misc.CommandLineState
{
    internal class EnvironmentVariableState : ArgumentState
    {
        public override async Task<bool> ArgumentProcessAsync(string[]? args = null)
        {
            string? AddressStart = Environment.GetEnvironmentVariable("ADDRESS_START");
            string? AddressMask = Environment.GetEnvironmentVariable("ADDRESS_MASK");
            return false;
        }
    }
}
