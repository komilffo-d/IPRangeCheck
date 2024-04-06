using IniParser;
using IniParser.Parser;
using IPRangeCheckConsole.Interfaces;
using Microsoft.Extensions.Configuration;

namespace IPRangeCheckConsole.Misc.CommandLineState
{
    internal class ConfigFileState : ArgumentState
    {
        public ConfigFileState(IFileWriter fileWriter, IFileReader fileReader) : base()
        {
            _fileWriter = fileWriter;
            _fileReader = fileReader;
        }

        private protected override Task<CLIOptions> GetParameters(string[]? args = null)
        {
            if (!File.Exists("config.ini"))
                throw new FileNotFoundException("Не найден конфигурационный файл!");
            FileIniDataParser parser = new FileIniDataParser();

            parser.ReadFile("config.ini");


            return null!;
        }
    }
}
