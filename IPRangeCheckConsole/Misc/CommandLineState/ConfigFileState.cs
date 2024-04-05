using IniParser;
using IniParser.Parser;
using Microsoft.Extensions.Configuration;

namespace IPRangeCheckConsole.Misc.CommandLineState
{
    public class ConfigFileState : ArgumentState
    {
        public override async Task<bool> ArgumentProcessAsync(string[]? args = null)
        {
            if (!File.Exists("config.ini"))
                throw new FileNotFoundException("Не найден конфигурационный файл!");
            FileIniDataParser parser = new FileIniDataParser();

            parser.ReadFile("config.ini");


            return true;
        }
    }
}
