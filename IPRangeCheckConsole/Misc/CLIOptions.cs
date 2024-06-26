﻿using CommandLine;

namespace IPRangeCheckConsole.Misc
{
    public class CLIOptions
    {

        [Option(shortName: 'l', longName: "file-log", HelpText = "Путь к файлу с логами")]
        public string FileLog { get; set; } 


        [Option(shortName: 'o', longName: "file-output", HelpText = "Путь к файлу с результатом")]
        public string FileOutput { get; set; } 

        [Option(shortName: 's', longName: "address-start", HelpText = "Нижняя граница диапазона адресов, необязательный параметр, по умолчанию обрабатываются все адреса")]
        public string AddressStart { get; set; } 

        [Option(shortName: 'm', longName: "address-mask", HelpText = "Маска подсети, задающая верхнюю границу диапазона десятичное число. Необязательный параметр. В случае, если он не указан, обрабатываются все адреса, начиная с нижней границы диапазона. Параметр нельзя использовать, если не задан address-start")]
        public string AddressMask { get; set; }

        [Option(shortName: 't', longName: "time-start", HelpText = "Нижняя граница временного интервала")]
        public string TimeStart { get; set; }

        [Option(shortName: 'e', longName: "time-end", HelpText = "Верхняя граница временного интервала")]
        public string TimeEnd { get; set; } 


    }
}
