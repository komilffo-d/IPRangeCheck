using Microsoft.Extensions.Configuration;

namespace IPRangeCheckConsole.Misc.CommandLineState
{
    public class ConfigFileState : ArgumentState
    {
        public override async Task<bool> ArgumentProcessAsync(string[]? args = null)
        {
            IConfiguration config = new ConfigurationBuilder()
                        .AddIniFile("config.ini")
                        .Build();

            IConfigurationSection section = config.GetSection("File");
            IConfigurationSection address = config.GetSection("Address");

            Console.WriteLine("Ошибка");
            return true;
        }
    }
}
