using CommandLine;
using IPRangeCheckConsole.Interfaces;
using NetTools;
using System.Globalization;
using System.Net;

namespace IPRangeCheckConsole.Misc.CommandLineState
{
    internal class CommandLineState : ArgumentState
    {
        private readonly IFileWriter _fileWriter;
        private readonly IFileReader _fileReader;


        public CommandLineState(IFileWriter fileWriter, IFileReader fileReader)
        {
            _fileWriter = fileWriter;
            _fileReader = fileReader;
        }
        public override async Task<bool> ArgumentProcessAsync(string[]? args = null)
        {

            Parser parser = new Parser(ps => ps.ParsingCulture = CultureInfo.DefaultThreadCurrentCulture);

            await parser.ParseArguments<CLIOptions>(args).MapResult(async (CLIOptions opts) =>
            {

                var (fileLog, fileOutput, AddressStart, AddressMask, timeStart, timeEnd) = (opts.FileLog, opts.FileOutput, opts.AddressStart, opts.AddressMask, opts.TimeStart, opts.TimeEnd.AddDays(1).AddTicks(-1));

                Dictionary<string, int> dictIpAddresses = new Dictionary<string, int>();

                IPAddressRange rangeIP = IPAddressRange.Parse($"{AddressStart}/{AddressMask}");

                string? key = null;


                await foreach (string IP in _fileReader.ReadAsync(fileLog))
                {
                    string[] lineData = IP.Split('|');
                    string? ipAddress = lineData.FirstOrDefault();
                    DateTime dateTime = DateTime.Parse(lineData.LastOrDefault());
                    key = $"{ipAddress} {dateTime.ToString("dd.MM.yyyy")}";


                    if (dictIpAddresses.ContainsKey(key.ToString()))
                        dictIpAddresses[key.ToString()]++;
                    else if (dateTime < timeEnd && dateTime >= timeStart && rangeIP.Contains(IPAddress.Parse(ipAddress)))
                        dictIpAddresses.Add(key.ToString(), 1);
                }

                _fileWriter.WriteAsync(fileOutput, dictIpAddresses.Select(t => $"{t.Key} Количество обращений: {t.Value}"));


            }, async err =>
            {
                _context.SwitchToState(new ConfigFileState());
                await _context.ArgumentProcessAsync(args);
            });
            return true;
        }
    }
}
