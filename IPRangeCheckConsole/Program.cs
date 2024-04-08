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


namespace IPRangeCheckConsole
{
    internal sealed class Program
    {
        private static bool IsSuccess { get; set; } = false;

        private static void NullProperty(CLIOptions options, List<string> errorsList)
        {
            foreach (var error in errorsList)
            {
                switch (error)
                {
                    case "FileLog":
                        options.FileLog = null;
                        break;
                    case "FileOutput":
                        options.FileOutput = null;
                        break;
                    case "AddressStart":
                        options.AddressStart = null;
                        break;
                    case "AddressMask":
                        options.AddressMask = null;
                        break;
                    case "TimeStart":
                        options.TimeStart = null;
                        break;
                    case "TimeEnd":
                        options.TimeEnd = null;
                        break;
                    default:
                        break;
                }
            }
            errorsList.Clear();

        }
        static async Task Main(string[] args)
        {
            Startup.Initialize();
            /*  await Startup.GenerateFileLog("C:\\Users\\Daniil\\Downloads\\", new IPGenerator("192.168.0.0", "192.168.0.10"), new DateTimeGenerator(new DateTime(2001, 3, 1), new DateTime(2001, 3, 30)), 10000);*/
            try
            {

                Log.Information("Начато формирование хоста.");
                IHost host = CreateHostBuilder(args).Build();
                Log.Information("Хост собран.");


                var conf = host.Services.GetService<IConfiguration>();

                CLIOptionsValidator _validator = new CLIOptionsValidator();
                CLIOptions cliOptions = new CLIOptions()
                {
                    FileLog = conf["FILE_LOG"],
                    FileOutput = conf["FILE_OUTPUT"],
                    AddressStart = conf["ADDRESS_START"],
                    AddressMask = conf["ADDRESS_MASK"],
                    TimeStart = DateOnly.TryParseExact(conf["TIME_START"], "dd.MM.yyyy", null, DateTimeStyles.None, out DateOnly timeStart) ? timeStart : null,
                    TimeEnd = DateOnly.TryParseExact(conf["TIME_END"], "dd.MM.yyyy", null, DateTimeStyles.None, out DateOnly timeEnd) ? timeEnd : null,
                };
                var result = _validator.Validate(cliOptions);
                if (result.Errors.Count > 0)
                    Log.Error("Переданы не все аргументы или переданы в неверном формате:");
                foreach (var error in result.Errors)
                    Log.Error($"--> {error.ErrorMessage} <--");



                /*
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
                */







            }
            catch (Exception ex)
            {
                Log.Fatal("Произошла критическая ошибка выполнения программы! Просим перезапустить ПО." + ex);
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
                var dictArg = new Dictionary<string, string>()
                {
                    { "--file-log", "FILE_LOG" },
                    { "--file-output", "FILE_OUTPUT" },
                    { "--address-start", "ADDRESS_START" },
                    { "--address-mask", "ADDRESS_MASK" },
                    { "--time-start", "TIME_START" },
                    { "--time-end", "TIME_END" },
                };

                configuration.AddCommandLine(args, dictArg)
                               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                               .AddEnvironmentVariables();

                Log.Information("Переменные среды успешно добавлены!");
                if (args.Length > 0)
                    Log.Information("Аргументы командной строки загружены в приложение успешно!");
                /*                if (!File.Exists("appsettings.json"))
                                {
                                    Log.Warning("Конфигурационный файл не найден!");

                                }
                                else
                                {
                                    JsonSchema jsonSchema = JsonSchema.FromType<CLIOptionsSchema>();
                                    string? jsonStrFromFile = await File.ReadAllTextAsync("appsettings.Json");
                                    var errors = jsonSchema.Validate(jsonStrFromFile);

                                    if (errors.Count != 0)
                                    {
                                        Log.Warning("Конфигурационный файл имеет неверный формат данных!");
                                    }
                                    else
                                    {

                                        Log.Information("Конфигурационный файл загружен в приложение успешно!");
                                    }
                                }*/




            }).ConfigureServices((app, services) =>
            {

                services.AddScoped<IFileReader, FileService>()
                        .AddScoped<IFileWriter, FileService>()
                        .AddScoped<IValidator<CLIOptions>, CLIOptionsValidator>();
            });
        }

    }
}
