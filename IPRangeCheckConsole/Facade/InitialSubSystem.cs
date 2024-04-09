using FluentValidation;
using IPRangeCheckConsole.Interfaces;
using IPRangeCheckConsole.Misc;
using IPRangeCheckConsole.Services;
using IPRangeCheckConsole.Validators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Globalization;

namespace IPRangeCheckConsole.Facade
{
    internal class InitialSubSystem
    {
        private static InitialSubSystem _instance;
        private InitialSubSystem()
        {

        }
        public static InitialSubSystem CreateInstance()
        {
            if (_instance == null)
                _instance = new InitialSubSystem();
            return _instance;
        }
        public void Initialize()
        {

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console(outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz}} {Level}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();

            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ru-RU");
            ValidatorOptions.Global.LanguageManager.Culture = CultureInfo.DefaultThreadCurrentCulture;
        }
        public IHostBuilder CreateHostBuilder(string[] args)
        {
            Log.Information("Начато формирование хоста...");
            return Host.CreateDefaultBuilder(args).ConfigureAppConfiguration((app, configuration) =>
            {
                var dictArg = new Dictionary<string, string>()
                {
                    { "--file-log", "FILE_LOG" },
                    { "--file-output", "FILE_OUTPUT" },
                    { "--address-start", "ADDRESS_START" },
                    { "--address-mask", "ADDRESS_MASK" },
                    { "--time-start", "TIME_START" },
                    { "--time-end", "TIME_END" },
                };


                if (FileTemplateValidParse.IsParseIniAsync("config.ini").GetAwaiter().GetResult())
                    configuration.AddIniFile("config.ini", optional: true, reloadOnChange: true);
                if (FileTemplateValidParse.IsParseJsonAsync("appsettings.json").GetAwaiter().GetResult())
                    configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                configuration.AddEnvironmentVariables();
                configuration.AddCommandLine(args, dictArg);
                Log.Information("Формирование хоста завершено.");

            }).ConfigureServices((app, services) =>
            {

                services.AddScoped<IFileReader, FileService>()
                        .AddScoped<IFileWriter, FileService>()
                        .AddScoped<IValidator<CLIOptions>, CLIOptionsValidator>();
            });
        }

    }
}
