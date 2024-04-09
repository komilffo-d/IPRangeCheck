using IniParser;
using IniParser.Exceptions;
using IniParser.Model.Configuration;
using IniParser.Parser;
using IPRangeCheckConsole.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using System.Text;

namespace IPRangeCheckConsole.Services
{
    public static class FileTemplateValidParse
    {
        private const string JSON_EXTENSION = ".json";

        private const string INI_EXTENSION = ".ini";
        private static readonly Lazy<IFileReader> _fileReader = new Lazy<IFileReader>(() => new FileService());

        public static async Task<bool> IsParseJsonAsync(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Log.Warning("Файл JSON не найден!");
                return false;
            }

            if (Path.GetExtension(filePath) != JSON_EXTENSION)
            {
                Log.Warning("Предоставленный файл имеет расширением не .json!");
                return false;
            }
            bool isSuccess = false;
            StringBuilder jsonContentBuilder = new StringBuilder();
            await foreach (string line in _fileReader.Value.ReadAsync(filePath))
                jsonContentBuilder.Append(line);

            string jsonContent = jsonContentBuilder.ToString();
            try
            {
                JContainer.Parse(jsonContent);
                isSuccess = true;
                Log.Information("Файл JSON является пригодным для использования!");
            }
            catch(JsonReaderException)
            {
                isSuccess = false;
                Log.Warning("Файл JSON не является пригодным для использования!");

            }
            return isSuccess;
        }

        public static bool IsParseIni(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Log.Warning("Файл INI не найден!");
                return false;
            }

            if (Path.GetExtension(filePath) != INI_EXTENSION)
            {
                Log.Warning("Предоставленный файл имеет расширением не .ini!");
                return false;
            }

            bool isSuccess = false;
            FileIniDataParser iniParser = new FileIniDataParser();
            try
            {
                iniParser.ReadFile(filePath);
                isSuccess = true;
                Log.Information("Файл INI является пригодным для использования!");
            }
            catch(ParsingException)
            {
                isSuccess = false;
                Log.Warning("Файл INI не является пригодным для использования!");

            }
            return isSuccess;
        }
    }
}
