using IPRangeCheckConsole.Interfaces;
using NetTools;
using System.Net;

namespace IPRangeCheckConsole.Misc.CommandLineState
{
    //Паттерн "Стратегия"
    public abstract class ArgumentStrategy
    {
        private protected  IFileWriter _fileWriter;
        private protected  IFileReader _fileReader;


        private protected ArgumentStrategy()
        {

        }

        //Паттерн "Шаблонный метод"
        public async Task<bool> ArgumentProcessAsync(string[]? args = null)
        {
            CLIOptions options = await GetParameters(args);
            if (options is null)
                return false;
            Dictionary<string, int> dictIpAddresses = new Dictionary<string, int>();

            IPAddressRange rangeIP = IPAddressRange.Parse($"{options.AddressStart}/{options.AddressMask}");
            string? key = null;

            await foreach (string IP in _fileReader.ReadAsync(options.FileLog))
            {
                string[] lineData = IP.Split('|');
                string? ipAddress = lineData.FirstOrDefault();
                DateTime dateTime = DateTime.Parse(lineData.LastOrDefault());
                key = $"{ipAddress} {dateTime.ToString("dd.MM.yyyy")}";


                if (dictIpAddresses.ContainsKey(key.ToString()))
                    dictIpAddresses[key.ToString()]++;
                else if (dateTime < options.TimeEnd && dateTime >= options.TimeStart && rangeIP.Contains(IPAddress.Parse(ipAddress)))
                    dictIpAddresses.Add(key.ToString(), 1);
            }

            await _fileWriter.WriteAsync(options.FileOutput, dictIpAddresses.Select(t => $"{t.Key} Количество обращений: {t.Value}"));

            return true;
        }

        private protected abstract Task<CLIOptions> GetParameters(string[]? args = null);

    }
}
