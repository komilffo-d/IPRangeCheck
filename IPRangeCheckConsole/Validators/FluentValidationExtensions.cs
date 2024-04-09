using FluentValidation;
using IPRangeCheckConsole.Misc;
using IPRangeGenerator.Misc;
using Newtonsoft.Json.Linq;
using System.Net;

namespace IPRangeCheckConsole.Validators
{
    public static class FluentValidationExtensions
    {
        public static IRuleBuilderOptions<T, string> ValidateIPAddress<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                       .Must(val =>
                       {

                           return IPAddressUtility.IsValidIPv4(val) && IPAddress.TryParse(val, out _);

                       }).WithMessage("У свойства '{PropertyName}' неправильно задан формат IP-адреса.");
        }

        public static IRuleBuilderOptions<T, string> ValidateMaskIPAddress<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                       .Must(val =>
                       {
                           if (IPAddress.TryParse(val, out var ipAddress))
                           {
                               var bytes = ipAddress.GetAddressBytes();
                               bool onlyOne = false;
                               for (int i = bytes.Length - 1; i >=0; i--)
                               {
                                   for (int j = 0; j < 8; j++)
                                   {

                                       bool bit = ((bytes[i] >> j) & 1) == 1;
                                       if (bit == false && onlyOne == true)
                                           return false;
                                       onlyOne = bit;

                                   }
                               }
                               return true;
                           }

                           return false;

                       }).WithMessage("У свойства '{PropertyName}' неправильно задана IP-маска.");
        }

        public static IRuleBuilderOptions<T, string> ValidateFilePath<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                       .Must(val =>
                       {

                           FileInfo fileInfo = new FileInfo(val);

                           if (!fileInfo.Exists)
                               return false;
                           return true;
                       }).WithMessage("У свойства '{PropertyName}' неправильно задан путь к файлу.");

        }

        public static IRuleBuilderOptions<T, string> ValidateDirectoryPath<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                       .Must(val =>
                       {
                           if (Path.IsPathFullyQualified(val) && !Path.GetInvalidPathChars().Any(val.Contains) && Directory.Exists(Path.GetDirectoryName(val)))
                           {
                               return true;
                           }
                           return false;
                       }).WithMessage("У свойства '{PropertyName}' неправильно задан путь к директории.");

        }

        public static IRuleBuilderOptions<T, string> ValidateDateOnly<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                       .Must(val =>
                       {
                           var key = DateOnly.TryParseExact(val?.ToString(), "dd.MM.yyyy", out DateOnly dateOnly);

                           return key;
                       }).WithMessage("Свойство '{PropertyName}' должно иметь формат даты dd.MM.yyyy.");

        }

    }
}
