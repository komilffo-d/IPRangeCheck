using IPRangeCheckConsole.Interfaces;
using IPRangeCheckConsole.Misc.CommandLineState;
using IPRangeCheckConsole.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;

namespace IPRangeCheckConsole
{
    internal sealed class Program
    {
        private static bool IsSuccess { get; set; } = false;
        static async Task Main(string[] args)
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ru-RU");
            try
            {
                /*                FileGenerator.CreateEntity();
                                FileGenerator? generator = FileGenerator.GetEntity();
                                generator?.SetIPGenerator(new IPGenerator("192.168.0.9", "192.168.10.100"));
                                generator?.SetDateTimeGenerator(new DateTimeGenerator("01.04.2001 06:08:20", "01.04.2001 06:15:20"));
                                await generator?.WriteFileAsync(@"C:\Users\Daniil\Downloads\log.txt", 100000, true);*/

                IHost host = CreateHostBuilder(args).Build();

                List<ArgumentState> listState = new List<ArgumentState>()
                {
                    ActivatorUtilities.CreateInstance<CommandLineState>(host.Services),
                    ActivatorUtilities.CreateInstance<ConfigFileState>(host.Services),
                    ActivatorUtilities.CreateInstance<EnvironmentVariableState>(host.Services)
                };
                CommandLineContext CLIContext = new CommandLineContext(listState.First());
                foreach (ArgumentState item in listState)
                {

                    CLIContext.SwitchToState(item);
                }




                IsSuccess = await CLIContext.ArgumentProcessAsync(args);



            }
            catch (Exception exception)
            {
                await Console.Out.WriteLineAsync(exception.ToString()).ContinueWith(async (task) => await Console.Out.WriteLineAsync("Сочувствуем,что произошла ошибка!"), TaskContinuationOptions.ExecuteSynchronously);

            }
            finally
            {
                await Console.Out.WriteLineAsync("Удачного дня!");
            }

        }
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureAppConfiguration((app, configuration) =>
            {
                configuration.Sources.Clear();
                configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            }).ConfigureServices((app, services) =>
            {
                services.AddScoped<IFileReader, FileService>()
                        .AddScoped<IFileWriter, FileService>();
            });
        }

    }
}
