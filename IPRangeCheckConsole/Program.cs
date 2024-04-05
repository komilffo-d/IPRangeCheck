using CommandLine;
using IPRangeCheckConsole.Interfaces;
using IPRangeCheckConsole.Misc.CommandLineState;
using IPRangeCheckConsole.Services;
using IPRangeGenerator;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace IPRangeCheckConsole
{
    internal sealed class Program
    {
        static Program()
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ru-RU");


        }
        static async Task Main(string[] args)
        {
            using ServiceProvider serviceProvider = new ServiceCollection()
            .AddScoped<IFileReader, FileService>()
            .AddSingleton<IFileWriter, FileService>()
            .AddTransient<CommandLineState>()
            .BuildServiceProvider();

            try
            {
                FileGenerator.CreateEntity();

                FileGenerator? generator = FileGenerator.GetEntity();
                generator?.SetIPGenerator(new IPGenerator("192.168.0.9", "192.168.10.100"));
                generator?.SetDateTimeGenerator(new DateTimeGenerator());
                await generator?.WriteFileAsync(@"C:\Users\Daniil\Downloads\outputTest.txt", 100000, true);

    
/*                CommandLineState commandLineState = serviceProvider.GetRequiredService<CommandLineState>();
                CommandLineContext CLIContext = new CommandLineContext(commandLineState);
                await CLIContext.ArgumentProcessAsync(args);
*/
            }
            catch (Exception exception)
            {
                await Console.Out.WriteLineAsync(exception.ToString());

            }
            finally
            {

            }






        }

    }
}
