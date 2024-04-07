using FluentValidation;
using IPRangeCheckConsole.Services;
using IPRangeGenerator;
using Serilog;
using System.Globalization;

namespace IPRangeCheckConsole
{
    internal static  class Startup
    {

        public static void Initialize()
        {

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console(outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz}} {Level}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();

            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ru-RU");
            ValidatorOptions.Global.LanguageManager.Culture = CultureInfo.DefaultThreadCurrentCulture;
        }

        public static async Task GenerateFileLog(string filePath, IPGenerator IPgenerator, DateTimeGenerator DateTimegenerator, int count = 100, bool isInclusive = true)
        {
            try
            {
                string? dir = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(Path.GetDirectoryName(dir))){
                    Log.Error($"Сгенерировать файл логов не выйдет из-за неправильного пути '{filePath}'.");
                    return;
                }

                string randomFilePath = $@"{filePath}\{Path.GetRandomFileName()}.txt";
                FileGenerator.CreateEntity();
                FileGenerator generator = FileGenerator.GetEntity()!;
                generator.SetIPGenerator(IPgenerator);
                generator.SetDateTimeGenerator(DateTimegenerator);
                await generator.WriteFileAsync(randomFilePath, count, isInclusive);

                Log.Information($@"Файл успешно снегерирован по пути '{randomFilePath}'");
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
