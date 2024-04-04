using CommandLine;
using IPRangeCheckConsole.Interfaces;
using IPRangeCheckConsole.Misc;
using IPRangeCheckConsole.Services;
using IPRangeGenerator;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Net;

namespace IPRangeCheckConsole
{
    internal sealed class Program
    {
        static Program()
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ru-RU");

            ServiceProvider serviceProvider = new ServiceCollection()
            .AddSingleton<IFileReader, FileService>()
            .AddSingleton<IFileWriter, FileService>()
            .BuildServiceProvider();
        }
        static async Task Main(string[] args)
        {
            await Parser.Default.ParseArguments<CLIOptions>(args).MapResult(async (CLIOptions opts) =>
            {
                FileService fileService = new FileService();


                IPGenerator generator;
                DateTimeGenerator generator2;
                try
                {
                    generator = new IPGenerator("100.10.0.0", "192.168.255.255");

                    generator2 = new DateTimeGenerator("2.4.2001 6:8:20", "6.4.2001 12:45:30");

                    IEnumerable<IPAddress> ipAddresses = generator.GenerateEnumerableInRange(100);

                    IEnumerable<DateTime> dateTimes = generator2.GenerateEnumerableInRange(100, true);

                    var collection = ipAddresses.Zip(dateTimes, (ip, dateTime) => new Tuple<IPAddress, DateTime>(ip, dateTime));

                    foreach (var item in collection)
                    {
                        Console.WriteLine(string.Format("{0, 20}  ||  {1,30}", item.Item1, item.Item2));

                    }
                    fileService.Write(collection.Select(t => $"{t.Item1} {t.Item2}"));

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    Console.WriteLine("\n\nПрограмма завершена!");
                }
            },err => Task.FromResult(-1));





        }
    }
}
