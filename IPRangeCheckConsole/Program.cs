using CommandLine;
using IPRangeCheckConsole.Misc;
using IPRangeCheckConsole.Services;
using IPRangeGenerator;
using Microsoft.Extensions.Configuration;
using NetTools;
using System.Globalization;
using System.Net;
using System.Text;

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
                DateTime timeStart = DateTime.Parse(opts.TimeStart);
                DateTime timeEnd = DateTime.Parse(opts.TimeEnd).AddDays(1).AddTicks(-1);

                FileService fileService = new FileService();
                Dictionary<string, int> dictIpAddresses = new Dictionary<string, int>();
                IPAddressRange rangeIP = IPAddressRange.Parse($"{AddressStart}/{AddressMask}");

                StringBuilder key = new StringBuilder();


                await foreach (string IP in fileService.ReadAsync(opts.FileLog))
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

                fileService.Write(opts.FileOutput, dictIpAddresses.Select(t => $"{t.Key} Количество обращений: {t.Value}"));

                /*                IPGenerator generator;
                                DateTimeGenerator generator2;
                                try
                                {
                                    generator = new IPGenerator("192.168.0.10", "192.168.1.19");

                                    generator2 = new DateTimeGenerator("2.4.2001 6:8:20", "6.4.2001 6:15:00");

                                    IEnumerable<IPAddress> ipAddresses = generator.GenerateEnumerableInRange(10000, true);

                                    IEnumerable<DateTime> dateTimes = generator2.GenerateEnumerableInRange(10000, true);

                                    var collection = ipAddresses.Zip(dateTimes, (ip, dateTime) => new Tuple<IPAddress, DateTime>(ip, dateTime));

                                    fileService.Write(opts.FileLog, collection.Select(t => $"{t.Item1}|{t.Item2.ToString("yyyy-MM-dd HH:mm:ss")}"));

                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                }
                                finally
                                {
                                    Console.WriteLine("\n\nПрограмма завершена!");
                                }*/
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
