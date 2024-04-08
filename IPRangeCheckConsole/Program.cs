﻿using FluentValidation;
using IPRangeCheckConsole.Interfaces;
using IPRangeCheckConsole.Misc;
using IPRangeCheckConsole.Services;
using IPRangeCheckConsole.Validators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetTools;
using Serilog;
using System.Globalization;
using System.Net;


namespace IPRangeCheckConsole
{
    internal sealed class Program
    {
        private static bool IsSuccess { get; set; } = false;

        static async Task Main(string[] args)
        {
            Startup.Initialize();
            /*  await Startup.GenerateFileLog("C:\\Users\\Daniil\\Downloads\\", new IPGenerator("192.168.0.0", "192.168.0.10"), new DateTimeGenerator(new DateTime(2001, 3, 1), new DateTime(2001, 3, 30)), 10000);*/
            IHost host = null;
            try
            {
                Log.Information("Начато формирование хоста...");
                host = CreateHostBuilder(args).Build();
                Log.Information("Хост собран.");
            }
            catch
            {
                Log.Fatal("Хост не собран!.Неправильно перадан сформированный JSON или INI файл.");
                Console.ReadKey();
                return;
            }

            var configuration = host.Services.GetService<IConfiguration>();
            var _fileReader = host.Services.GetService<IFileReader>();
            var _fileWriter = host.Services.GetService<IFileWriter>();

            CLIOptionsValidator _validator = new CLIOptionsValidator();
            CLIOptions cliOptions = new CLIOptions()
            {
                FileLog = configuration["FILE_LOG"],
                FileOutput = configuration["FILE_OUTPUT"],
                AddressStart = configuration["ADDRESS_START"] ?? "0.0.0.0",
                AddressMask = configuration["ADDRESS_MASK"] ?? "0.0.0.0",
                TimeStart = DateOnly.TryParseExact(configuration["TIME_START"], "dd.MM.yyyy", null, DateTimeStyles.None, out DateOnly timeStart) ? timeStart : null,
                TimeEnd = DateOnly.TryParseExact(configuration["TIME_END"], "dd.MM.yyyy", null, DateTimeStyles.None, out DateOnly timeEnd) ? timeEnd : null,
            };
            var result = _validator.Validate(cliOptions);

            IsSuccess = !(result.Errors.Count > 0);

            if (!IsSuccess)
            {
                Log.Error("Переданы не все аргументы или переданы в неверном формате:");
                foreach (var error in result.Errors)
                    Log.Error($"--> {error.ErrorMessage} <--");
            }
            else
            {
                Dictionary<string, int> dictIpAddresses = new Dictionary<string, int>();
                IPAddressRange rangeIP = IPAddressRange.Parse($"{cliOptions.AddressStart}/{cliOptions.AddressMask}");
                string? key = null;
                try
                {
                    await foreach (string IP in _fileReader.ReadAsync(cliOptions.FileLog))
                    {
                        string[] lineData = IP.Split('|');
                        string? ipAddress = lineData.FirstOrDefault();
                        DateOnly dateTime = DateOnly.FromDateTime(DateTime.Parse(lineData.LastOrDefault()));
                        key = $"{ipAddress} {dateTime.ToString("dd.MM.yyyy")}";


                        if (dictIpAddresses.ContainsKey(key.ToString()))
                            dictIpAddresses[key.ToString()]++;
                        else if (true && rangeIP.Contains(IPAddress.Parse(ipAddress)))
                            dictIpAddresses.Add(key.ToString(), 1);
                    }

                    await _fileWriter.WriteAsync(cliOptions.FileOutput, dictIpAddresses.Select(t => $"{t.Key} Count: {t.Value}"));
                }
                catch
                {
                    Log.Error("Возникла ошибка во время записи файла. Возможно, передан неверный входной файл.");
                }


            }

            if (IsSuccess)
                Log.Information("Работа приложения выполнена успешно!");
            else
                Log.Error("Приложение не выполнило своих обязанностех!");

            Log.Information("Нажмите любую клавищу, что закрыть программу: ");

            Console.ReadKey();
        }
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureAppConfiguration(async (app, configuration) =>
            {
                var dictArg = new Dictionary<string, string>()
                {
                    { "--file-log", "FILE_LOG" },
                    { "--file-output", "FILE_OUTPUT" },
                    { "--address-start", "ADDRESS_START" },
                    { "--address-mask", "ADDRESS_MASK" },
                    { "--time-start", "TIME_START" },
                    { "--time-end", "TIME_END" },
                };

                configuration.AddCommandLine(args, dictArg)
                    .AddIniFile("config.ini", optional: true, reloadOnChange: true)
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                   .AddEnvironmentVariables();



                Log.Information("Переменные среды успешно добавлены!");
                if (args.Length > 0)
                    Log.Information("Аргументы командной строки загружены в приложение успешно!");
            }).ConfigureServices((app, services) =>
            {

                services.AddScoped<IFileReader, FileService>()
                        .AddScoped<IFileWriter, FileService>()
                        .AddScoped<IValidator<CLIOptions>, CLIOptionsValidator>();
            });
        }

    }
}
