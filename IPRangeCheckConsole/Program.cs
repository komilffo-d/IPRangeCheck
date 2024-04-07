using FluentValidation;
using IPRangeCheckConsole.Interfaces;
using IPRangeCheckConsole.Misc;
using IPRangeCheckConsole.Misc.CommandLineState;
using IPRangeCheckConsole.Services;
using IPRangeCheckConsole.Validators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NJsonSchema;
using Serilog;


namespace IPRangeCheckConsole
{
    internal sealed class Program
    {
        private static bool IsSuccess { get; set; } = false;

        static async Task Main(string[] args)
        {
            Startup.Initialize();
            /*  await Startup.GenerateFileLog("C:\\Users\\Daniil\\Downloads\\", new IPGenerator("192.168.0.0", "192.168.0.10"), new DateTimeGenerator(new DateTime(2001, 3, 1), new DateTime(2001, 3, 30)), 10000);*/
            try
            {

                Log.Information("Начато формирование хоста.");
                IHost host = CreateHostBuilder(args).Build();
                Log.Information("Хост собран.");

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
            catch (Exception)
            {
                Log.Fatal("Произошла критическая ошибка выполнения программы! Просим перезапустить ПО.");
            }
            if (IsSuccess)
                Log.Information("Работа приложения выполнена успешно!");
            else
                Log.Error("Приложение не выполнило своих обязанностех!");

            Log.Information("Нажмите любую клавищу, что закрыть программу: ");

            Console.ReadKey();
        }
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureAppConfiguration(async (app, configuration) =>
            {
                configuration.Sources.Clear();

                if (!File.Exists("appsettings.json"))
                {
                    Log.Warning("Конфигурационный файл не найден!");
                    return;
                }


                JsonSchema jsonSchema = JsonSchema.FromType<CLIOptionsSchema>();
                string? jsonStrFromFile = await File.ReadAllTextAsync("appsettings.Json");
                var errors = jsonSchema.Validate(jsonStrFromFile);

                if (errors.Count != 0)
                {
                    Log.Warning("Конфигурационный файл имеет неверный формат данных!");
                    return;
                }
                configuration.AddEnvironmentVariables(s => s.Prefix = "SETTINGS");
                configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                Log.Information("Конфигурационный файл загружен в приложение успешно!");

                var dictArg = new Dictionary<string, string>()
                {
                    { "--file-log", "FILE_LOG" },
                    { "--file-output", "FILE_OUTPUT" },
                    { "--address-start", "ADDRESS_START" },
                    { "--address-mask", "ADDRESS_MASK" },
                    { "--time-start", "TIME_START" },
                    { "--time-end", "TIME_END" },
                };
                configuration.AddCommandLine(args, dictArg);

                Log.Information("Аргументы командной строки загружены в приложение успешно!");



                Log.Information("Переменные в приложение успешно!");


            }).ConfigureServices((app, services) =>
            {

                services.AddScoped<IFileReader, FileService>()
                        .AddScoped<IFileWriter, FileService>()
                        .AddScoped<IValidator<CLIOptions>, CLIOptionsValidator>();
            });
        }

    }
}
