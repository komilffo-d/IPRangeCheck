﻿using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPRangeCheckConsole.Misc
{
    internal class CLIOptions
    {
        [Option(shortName: 'l', longName:"file-log",Required = true, HelpText = "Путь к файлу с логами")]
         public string FileLog { get; set; }

        [Option(shortName: 'o', longName: "file-output", Required = true, HelpText = "Путь к файлу с результатом")]
        public string FileOutput { get; set; }

        [Option(shortName: 's', longName: "address-start", Required = false, HelpText = "Нижняя граница диапазона адресов, необязательный параметр, по умолчанию обрабатываются все адреса")]
        public string AddressStart { get; set; }

        [Option(shortName: 'm', longName: "address-mask", Required = false, HelpText = "Маска подсети, задающая верхнюю границу диапазона десятичное число. Необязательный параметр. В случае, если он не указан, обрабатываются все адреса, начиная с нижней границы диапазона. Параметр нельзя использовать, если не задан address-start")]
        public string AddressMask { get; set; }
    }
}
