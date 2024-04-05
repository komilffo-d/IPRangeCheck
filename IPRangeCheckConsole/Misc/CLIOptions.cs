using CommandLine;

namespace IPRangeCheckConsole.Misc
{
    internal class CLIOptions
    {

        [Option(shortName: 'l', longName: "file-log", Required = true, HelpText = "Путь к файлу с логами")]
        public string FileLog { get; set; }

        [Option(shortName: 'o', longName: "file-output", Required = true, HelpText = "Путь к файлу с результатом")]
        public string FileOutput { get; set; }


        [Option(shortName: 's', longName: "address-start", Required = false, HelpText = "Нижняя граница диапазона адресов, необязательный параметр, по умолчанию обрабатываются все адреса", Default = "0.0.0.0")]
        public string AddressStart { get; set; }

        [Option(shortName: 'm', longName: "address-mask", Required = false, Default = "0.0.0.0", HelpText = "Маска подсети, задающая верхнюю границу диапазона десятичное число. Необязательный параметр. В случае, если он не указан, обрабатываются все адреса, начиная с нижней границы диапазона. Параметр нельзя использовать, если не задан address-start")]
        public string AddressMask { get; set; }

        [Option(shortName: 't', longName: "time-start", Required = true, Default = "01.01.1900", HelpText = "Нижняя граница временного интервала")]
        public DateTime TimeStart { get; set; }

        [Option(shortName: 'e', longName: "time-end", Required = true, Default = "31.12.2099", HelpText = "Верхняя граница временного интервала")]
        public DateTime TimeEnd { get; set; }


    }
}
