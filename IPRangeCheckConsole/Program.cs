using CommandLine;
using IPRangeCheckConsole.Misc;
using IPRangeCheckConsole.Services;
using IPRangeGenerator;
using Microsoft.Extensions.Configuration;
using NetTools;
using System.Globalization;
using System.Net;

namespace IPRangeCheckConsole
{
    internal sealed class Program
    {
        static Program()
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ru-RU");
            /*
                        ServiceProvider serviceProvider = new ServiceCollection()
                        .AddSingleton<IFileReader, FileService>()
                        .AddSingleton<IFileWriter, FileService>()
                        .BuildServiceProvider();*/
        }
        static async Task Main(string[] args)
        {
            
            await Parser.Default.ParseArguments<CLIOptions>(args).MapResult(async (CLIOptions opts) =>
            {

                string AddressStart = opts.AddressStart ?? Environment.GetEnvironmentVariable("ADDRESS_START") ?? string.Empty;
                string AddressMask = opts.AddressMask ?? Environment.GetEnvironmentVariable("ADDRESS_MASK") ?? string.Empty;
                
                FileService fileService = new FileService();
                /*                Dictionary<string, int> dictIpAddresses = new Dictionary<string, int>();
                                IPAddressRange rangeIP = IPAddressRange.Parse($"{AddressStart}/{AddressMask}");


                                await foreach (string IP in fileService.ReadAsync(opts.FileLog))
                                {
                                    if (dictIpAddresses.ContainsKey(IP))
                                        dictIpAddresses[IP]++;
                                    else if (rangeIP.Contains(IPAddress.Parse(IP.Split(' ')?.FirstOrDefault()!)))
                                        dictIpAddresses.Add(IP, 1);
                                }

                                fileService.Write(opts.FileOutput, dictIpAddresses.Select(t => $"{t.Key} Количество обращений: {t.Value}"));*/

                IPGenerator generator;
                DateTimeGenerator generator2;
                try
                {
                    generator = new IPGenerator("192.168.0.10", "192.168.1.5");

                    generator2 = new DateTimeGenerator("2.4.2001 6:8:20", "2.4.2001 6:15:00");

                    IEnumerable<IPAddress> ipAddresses = generator.GenerateEnumerableInRange(10000, false);

                    IEnumerable<DateTime> dateTimes = generator2.GenerateEnumerableInRange(10000, false);

                    var collection = ipAddresses.Zip(dateTimes, (ip, dateTime) => new Tuple<IPAddress, DateTime>(ip, dateTime));

                    fileService.Write(opts.FileLog, collection.Select(t => $"{t.Item1} {t.Item2.ToString("d.M.yyyy H:m")}"));

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    Console.WriteLine("\n\nПрограмма завершена!");
                }
            }, err =>
            {
                IConfiguration config = new ConfigurationBuilder()
                                        .AddIniFile("config.ini")
                                        .Build();

                IConfigurationSection section = config.GetSection("File");
                IConfigurationSection address = config.GetSection("Address");

                Console.WriteLine("Ошибка");

                return Task.CompletedTask;
            });





        }
    }
}
