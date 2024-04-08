using FluentValidation;
using IPRangeCheckConsole.Interfaces;
using IPRangeCheckConsole.Misc;
using IPRangeCheckConsole.Misc.CommandLineState;
using IPRangeCheckConsole.Services;
using IPRangeCheckConsole.Validators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;


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

                var coll = conf.GetChildren();
                CLIOptionsValidator _validator = new CLIOptionsValidator();
                List<string> errorProperty = new List<string>();
                CLIOptions cliOptions = new CLIOptions()
                {
                    FileLog= conf["CMD_FILE_LOG"],
                    FileOutput = conf["CMD_FILE_OUTPUT"],
                    AddressStart = conf["CMD_ADDRESS_START"],
                    AddressMask = conf["CMD_ADDRESS_MASK"],
                    TimeStart = conf["CMD_TIME_START"],
                    TimeEnd = conf["CMD_TIME_END"],
                };

                var result=_validator.Validate(cliOptions);
                foreach (var error in result.Errors)
                {
                    errorProperty.Add(error.PropertyName);
               


                }

                NullProperty(cliOptions, errorProperty);

                cliOptions = new CLIOptions()
                {
                    FileLog = cliOptions.FileLog ?? conf["JSON_FILE_LOG"],
                    FileOutput = cliOptions.FileOutput ??conf["JSON_FILE_OUTPUT"],
                    AddressStart = cliOptions.AddressStart ?? conf["JSON_ADDRESS_START"],
                    AddressMask = cliOptions.AddressMask ?? conf["JSON_ADDRESS_MASK"],
                    TimeStart = cliOptions.TimeStart ?? conf["JSON_TIME_START"],
                    TimeEnd = cliOptions.TimeEnd ?? conf["JSON_TIME_END"],
                };

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
                var dictArg = new Dictionary<string, string>()
                {
                    { "--file-log", "CMD_FILE_LOG" },
                    { "--file-output", "CMD_FILE_OUTPUT" },
                    { "--address-start", "CMD_ADDRESS_START" },
                    { "--address-mask", "CMD_ADDRESS_MASK" },
                    { "--time-start", "CMD_TIME_START" },
                    { "--time-end", "CMD_TIME_END" },
                };

                configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                               .AddEnvironmentVariables()
                               .AddCommandLine(args, dictArg);

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
