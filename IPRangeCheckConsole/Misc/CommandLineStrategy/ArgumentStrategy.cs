using FluentValidation;
using FluentValidation.Results;
using IPRangeCheckConsole.Interfaces;
using NetTools;
using Serilog;
using System.Net;

namespace IPRangeCheckConsole.Misc.CommandLineState
{
    //Паттерн "Стратегия"
    public abstract class ArgumentStrategy
    {
        private protected  IFileWriter _fileWriter;
        private protected  IFileReader _fileReader;
        private protected IValidator<CLIOptions> _validator;

        private protected ArgumentStrategy()
        {

        }

        //Паттерн "Шаблонный метод"
        public async Task<bool> ArgumentProcessAsync(string[]? args = null)
        {

            CLIOptions options = await GetParameters(args);
            if (options is null)
                return false;
            ValidationResult result = await _validator.ValidateAsync(options);
            if (!result.IsValid)
                return false;
            Dictionary<string, int> dictIpAddresses = new Dictionary<string, int>();

            IPAddressRange rangeIP = IPAddressRange.Parse($"{options.AddressStart}/{options.AddressMask}");
            string? key = null;

            await foreach (string IP in _fileReader.ReadAsync(options.FileLog))
            {
                string[] lineData = IP.Split('|');
                string? ipAddress = lineData.FirstOrDefault();
                DateOnly dateTime = DateOnly.FromDateTime(DateTime.Parse(lineData.LastOrDefault()));
                key = $"{ipAddress} {dateTime.ToString("dd.MM.yyyy")}";


                if (dictIpAddresses.ContainsKey(key.ToString()))
                    dictIpAddresses[key.ToString()]++;
                else if (true && rangeIP.Contains(IPAddress.Parse(ipAddress)))
                    dictIpAddresses.Add(key.ToString(), 1);
            }

            await _fileWriter.WriteAsync(options.FileOutput, dictIpAddresses.Select(t => $"{t.Key} Count: {t.Value}"));

            return true;
        }

        private protected abstract Task<CLIOptions> GetParameters(string[]? args = null);

    }
}
