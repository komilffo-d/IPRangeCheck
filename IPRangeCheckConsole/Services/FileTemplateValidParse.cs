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
            StringBuilder jsonContent = new StringBuilder();
            await foreach (string line in _fileReader.Value.ReadAsync(filePath))
                jsonContent.Append(line);

            try
            {
                JObject.Parse(jsonContent.ToString());
                isSuccess = true;
                Log.Information("Файл JSON является пригодным для использования!");
            }
            catch (JsonReaderException ex)
            {
                isSuccess = false;
                Log.Warning("Файл JSON не является пригодным для использования!");

            }
            return isSuccess;
        }

        public static async Task<bool> IsParseIniAsync(string filePath)
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
            StringBuilder iniContent = new StringBuilder();
            await foreach (string line in _fileReader.Value.ReadAsync(filePath))
                iniContent.Append(line);
            IniDataParser iniParser = new IniDataParser(new IniParserConfiguration()
            {
                AllowKeysWithoutSection = false,
                AllowCreateSectionsOnFly = false,
                AllowDuplicateKeys = false,
                AllowDuplicateSections = false,
                SkipInvalidLines = false
            });
            try
            {
                iniParser.Parse(iniContent.ToString());
                isSuccess = true;
                Log.Information("Файл INI является пригодным для использования!");
            }
            catch (ParsingException ex)
            {
                isSuccess = false;
                Log.Information("Файл INI не является пригодным для использования!");

            }
            return isSuccess;
        }
    }
}
