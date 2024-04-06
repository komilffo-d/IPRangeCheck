using CommandLine;
using IPRangeCheckConsole.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace IPRangeCheckConsole.Misc
{
    internal class CLIOptions
    {
        [FilePathValidationAttribute]
        [Option(shortName: 'l', longName: "file-log", HelpText = "Путь к файлу с логами")]
        public string FileLog { get; set; }

        [FilePathValidationAttribute]
        [Option(shortName: 'o', longName: "file-output",  HelpText = "Путь к файлу с результатом")]
        public string FileOutput { get; set; }

        [IPAddressValidationAttribute]
        [Option(shortName: 's', longName: "address-start",  Default = "0.0.0.0", HelpText = "Нижняя граница диапазона адресов, необязательный параметр, по умолчанию обрабатываются все адреса")]
        public string? AddressStart { get; set; }

        [IPAddressValidationAttribute]
        [Option(shortName: 'm', longName: "address-mask",  Default = "0.0.0.0", HelpText = "Маска подсети, задающая верхнюю границу диапазона десятичное число. Необязательный параметр. В случае, если он не указан, обрабатываются все адреса, начиная с нижней границы диапазона. Параметр нельзя использовать, если не задан address-start")]
        public string? AddressMask { get; set; }

        [DateTimeValidationAttribute]
        [Option(shortName: 't', longName: "time-start",  Default = "01.01.1900", HelpText = "Нижняя граница временного интервала")]
        public DateTime TimeStart { get; set; }

        [DateTimeValidationAttribute]
        [Option(shortName: 'e', longName: "time-end", Default = "31.12.2099", HelpText = "Верхняя граница временного интервала")]
        public DateTime TimeEnd { get; set; }


    }
}
