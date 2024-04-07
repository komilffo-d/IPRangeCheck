using FluentValidation;
using IPRangeCheckConsole.Interfaces;
using IPRangeCheckConsole.Misc;
using IPRangeCheckConsole.Misc.CommandLineState;
using IPRangeCheckConsole.Services;
using IPRangeCheckConsole.Validators;
using IPRangeGenerator;
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
            ValidatorOptions.Global.LanguageManager.Culture = CultureInfo.DefaultThreadCurrentCulture;
            try
            {
                await GenerateFileLog("192.168.0.0", "192.168.0.10", new DateTime(2001, 3, 25), new DateTime(2001, 3, 30), 10000);
                IHost host = CreateHostBuilder(args).Build();

                List<ArgumentStrategy> listStrategy = new List<ArgumentStrategy>()
                {
                    ActivatorUtilities.CreateInstance<CommandLineStrategy>(host.Services),
                    ActivatorUtilities.CreateInstance<ConfigFileStrategy>(host.Services),
                    ActivatorUtilities.CreateInstance<EnvironmentVariableStrategy>(host.Services)
                };
                CommandLineContext CLIContext = new CommandLineContext();

                foreach (ArgumentStrategy item in listStrategy)
                {
                    CLIContext.SwitchToStrategy(item);
                    IsSuccess = await CLIContext.ArgumentProcessAsync(args);
                    if (IsSuccess)
                        break;
                }








            }
            catch (Exception exception)
            {
                await Console.Out.WriteLineAsync(exception.ToString()).ContinueWith(async (task) => await Console.Out.WriteLineAsync("Сочувствуем,что произошла ошибка!"), TaskContinuationOptions.ExecuteSynchronously);

            }
            finally
            {
                if (IsSuccess)
                    await Console.Out.WriteLineAsync("Работа приложения выполнена успешно!");
                else
                    await Console.Out.WriteLineAsync("Приложение не выполнило своих обязанностех!");
                Thread.Sleep(-1);
            }

        }
        public static async Task GenerateFileLog(string minIPAddress, string maxIPAddress, DateTime minDateTime, DateTime maxDateTime, int count = 100, bool isInclusive = true)
        {
            try
            {
                FileGenerator.CreateEntity();
                FileGenerator? generator = FileGenerator.GetEntity();
                generator?.SetIPGenerator(new IPGenerator(minIPAddress, maxIPAddress));
                generator?.SetDateTimeGenerator(new DateTimeGenerator(minDateTime, maxDateTime));
                await generator?.WriteFileAsync(@"C:\Users\Daniil\Downloads\log.txt", count, isInclusive);
            }
            catch (Exception)
            {

                throw;
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
                        .AddScoped<IFileWriter, FileService>()
                        .AddScoped<IValidator<CLIOptions>, CLIOptionsValidator>();
            });
        }

    }
}
