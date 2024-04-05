using IPRangeCheckConsole.Interfaces;
using IPRangeCheckConsole.Misc.CommandLineState;
using IPRangeCheckConsole.Services;
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
                CommandLineState commandLineState= serviceProvider.GetRequiredService<CommandLineState>();
                CommandLineContext CLIContext = new CommandLineContext(commandLineState);
                await CLIContext.ArgumentProcessAsync(args);

            }
            catch (Exception)
            {


            }
            finally
            {

            }






        }

    }
}
