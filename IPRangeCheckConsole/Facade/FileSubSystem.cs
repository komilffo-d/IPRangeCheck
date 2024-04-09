﻿using IPRangeCheckConsole.Interfaces;
using IPRangeCheckConsole.Misc;
using IPRangeCheckConsole.Services;
using IPRangeGenerator;
using IPRangeGenerator.Misc;
using NetTools;
using Serilog;
using System.Globalization;
using System.Net;

namespace IPRangeCheckConsole.Facade
{
    internal class FileSubSystem
    {
        private readonly IFileReader _fileReader;
        private readonly IFileWriter _fileWriter;

        public FileSubSystem(IFileReader fileReader, IFileWriter fileWriter)
        {
            _fileReader = fileReader;
            _fileWriter = fileWriter;
        }

        public async Task<Dictionary<string, int>> GetOutputDataAsync(CLIOptions cliOptions)
        {
            Dictionary<string, int> dictionaryIpAddresses = new Dictionary<string, int>();

            IPAddressRange rangeIP = IPAddressRange.Parse($"{cliOptions.AddressStart ?? "0.0.0.0"}/{cliOptions.AddressMask ?? "0.0.0.0"}");

            int countLine = 1;
            await foreach (string line in _fileReader.ReadAsync(cliOptions.FileLog))
            {
                string[] lineData = line.Split('|');
                if (lineData.Length != 2)
                    throw new InvalidDataException($"Неправильный формат входных данных! строка {countLine}");
                string? ipAddress = lineData.FirstOrDefault();
                if (string.IsNullOrEmpty(ipAddress) || !IPAddressUtility.IsValidIPv4(ipAddress) || !IPAddress.TryParse(ipAddress, out _))
                    throw new InvalidDataException($"Неправильный формат входных данных! строка {countLine}");
                string? dateTime = lineData.LastOrDefault();

                if (string.IsNullOrEmpty(dateTime) || !DateTime.TryParseExact(dateTime, "yyyy-MM-dd HH:mm:ss", null,DateTimeStyles.None, out DateTime tempDateTime))
                    throw new InvalidDataException($"Неправильный формат входных данных! строка {countLine}");

                DateOnly dateOnly = DateOnly.FromDateTime(tempDateTime);
                string key = $"{ipAddress} {dateOnly.ToString("dd.MM.yyyy")}";

                if (dictionaryIpAddresses.ContainsKey(key))
                    dictionaryIpAddresses[key]++;
                else if (dateOnly <= DateOnly.Parse(cliOptions.TimeEnd) && dateOnly >= DateOnly.Parse(cliOptions.TimeStart) && rangeIP.Contains(IPAddress.Parse(ipAddress)))
                    dictionaryIpAddresses.Add(key, 1);
                countLine++;
            }

            return dictionaryIpAddresses;
        }

        public async Task WriteOutputDataToFileAsync(string fileOutput, IEnumerable<KeyValuePair<string, int>> data)
        {
            try
            {
                await _fileWriter.WriteAsync(fileOutput, data.Select(t => $"{t.Key} Count: {t.Value}"));
            }
            catch (InvalidDataException ex)
            {
                Log.Error("Путь к файлу остутствует!");
                throw;
            }
            catch (Exception ex)
            {
                Log.Fatal("Возникла ошибка во время записи файла.", ex);
                throw;
            }
        }
        public async Task GenerateFileLogAsync(string filePath, IPGenerator IPgenerator, DateTimeGenerator DateTimegenerator, int count = 100, bool isInclusive = true)
        {
            try
            {
                string? dir = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(Path.GetDirectoryName(dir)))
                {
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
