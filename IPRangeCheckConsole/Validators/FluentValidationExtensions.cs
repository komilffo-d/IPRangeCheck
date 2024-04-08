using FluentValidation;
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

                           string[] octets = val.Split('.');


                           if (octets.Length != 4) return false;

                           foreach (var octet in octets)
                           {
                               int q;

                               if (!int.TryParse(octet, out q)
                                   || !q.ToString().Length.Equals(octet.Length)
                                   || q < 0
                                   || q > 255)
                                   return false;

                           }
                           return IPAddress.TryParse(val, out _);

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

        public static IRuleBuilderOptions<T, string?> ValidateDateTime<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                       .Must(val =>
                       {
                           var key = DateOnly.TryParseExact(val?.ToString(), "d.M.yyyy", out DateOnly dateOnly);

                           return key;
                       }).WithMessage("У свойства '{PropertyName}' неправильно задана дата или её формат.");

        }
    }
}
