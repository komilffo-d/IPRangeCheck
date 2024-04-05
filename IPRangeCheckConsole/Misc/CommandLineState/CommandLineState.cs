using CommandLine;
using IPRangeCheckConsole.Interfaces;
using IPRangeCheckConsole.Services;
using Microsoft.Extensions.Configuration;
using NetTools;
using System.Globalization;
using System.Net;
using System.Text;

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
        public override async Task<bool> ArgumentProcessAsync(string[]? args=null)
        {

            Parser parser = new Parser(ps => ps.ParsingCulture = CultureInfo.DefaultThreadCurrentCulture);
            
            await parser.ParseArguments<CLIOptions>(args).MapResult(async (CLIOptions opts) =>
            {

                string AddressStart = opts.AddressStart;
                string AddressMask = opts.AddressMask;
                DateTime timeStart = opts.TimeStart;
                DateTime timeEnd = opts.TimeEnd.AddDays(1).AddTicks(-1);

                
                Dictionary<string, int> dictIpAddresses = new Dictionary<string, int>();
                IPAddressRange rangeIP = IPAddressRange.Parse($"{AddressStart}/{AddressMask}");

                StringBuilder key = new StringBuilder();


                await foreach (string IP in _fileReader.ReadAsync(opts.FileLog))
                {
                    string[] lineData = IP.Split('|');
                    string? ipAddress = lineData.FirstOrDefault();
                    DateTime dateTime = DateTime.Parse(lineData.LastOrDefault());
                    key.Append($"{ipAddress} {dateTime.ToString("dd.MM.yyyy")}");


                    if (dictIpAddresses.ContainsKey(key.ToString()))
                        dictIpAddresses[key.ToString()]++;
                    else if (dateTime < timeEnd && dateTime >= timeStart && rangeIP.Contains(IPAddress.Parse(ipAddress)))
                        dictIpAddresses.Add(key.ToString(), 1);
                    key.Clear();
                }

                _fileWriter.WriteAsync(opts.FileOutput, dictIpAddresses.Select(t => $"{t.Key} Количество обращений: {t.Value}"));

               
            }, err =>
            {
                _context.SwitchToState(new ConfigFileState());

                return Task.CompletedTask;
            });
            return true;
        }
    }
}
