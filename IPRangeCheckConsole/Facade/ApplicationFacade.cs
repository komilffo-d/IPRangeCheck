using FluentValidation.Results;
using IPRangeCheckConsole.Misc;
using IPRangeGenerator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Reflection;

namespace IPRangeCheckConsole.Facade
{
    internal class ApplicationFacade
    {
        protected InitialSubSystem _initialSubSystem;
        protected FileSubSystem _fileSubSystem;
        protected OptionsSubSystem _optionsSubSystem;


        public ApplicationFacade(InitialSubSystem initialSubSystem)
        {
            _initialSubSystem = initialSubSystem;        
        }


        public async Task<int> RunAsync(string[] args)
        {
            try
            {
                _initialSubSystem.Initialize();
                var host = _initialSubSystem.CreateHostBuilder(args).Build();
                Log.Information("Переменные среды успешно добавлены!");
                if (args.Length > 0)
                    Log.Information("Аргументы командной строки загружены в приложение успешно!");
                Log.Information("Хост собран.");

                InitializeSubSystem(host.Services);

                // Раскомменитровать для генерации файла логов от 192.168.0.0 до 192.168.3.255 по диапозону IP-адрессов включительно и от 1 января 2001 00:00:00 до 28 февраля 2001 23:59:59 по дате включительно количеством в 10000 штук
/*                 await _fileSubSystem.GenerateFileLogAsync(Directory.GetCurrentDirectory(), new IPGenerator("192.168.0.0", "192.168.3.255"), new DateTimeGenerator(new DateTime(2001, 1, 1), new DateTime(2001, 2, 28)), 10000);*/

                CLIOptions cliOptions = _optionsSubSystem.GetCLIOptions(host.Services.GetRequiredService<IConfiguration>());

                ValidationResult validationResult = _optionsSubSystem.ValidateOptions(cliOptions);

                if (!validationResult.IsValid)
                {
                    _optionsSubSystem.LogValidationErrors(validationResult.Errors);
                    return 0;
                }

                var dictionaryIpAddresses = await _fileSubSystem.GetOutputDataAsync(cliOptions);
                await _fileSubSystem.WriteOutputDataToFileAsync(cliOptions.FileOutput, dictionaryIpAddresses);
                return 1;
            }
            catch (InvalidDataException ex)
            {
                Log.Error(ex.Message);
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal("Критическая ошибка в работе приложения!", ex);
                return -1;
            }
        }
        private void InitializeSubSystem(IServiceProvider serviceProvider)
        {
            _fileSubSystem = ActivatorUtilities.CreateInstance<FileSubSystem>(serviceProvider);
            _optionsSubSystem = ActivatorUtilities.CreateInstance<OptionsSubSystem>(serviceProvider);
            Log.Information("Все подсистемы системы инициализированы!");
        }
    }
}
