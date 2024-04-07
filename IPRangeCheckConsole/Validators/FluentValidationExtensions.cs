using FluentValidation;
using System.Net;

namespace IPRangeCheckConsole.Validators
{
    public static class FluentValidationExtensions
    {
        public static IRuleBuilderOptions<T, string> ValidateIPAddress<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                       .Must(val => IPAddress.TryParse(val, out _)).WithMessage("У свойства {PropertyName} неправильно задан IP-Address.");
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
                       }).WithMessage("У свойства {PropertyName} неправильно задан путь к файлу.");

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
                       }).WithMessage("У свойства {PropertyName} неправильно задан путь к директории.");

        }

        public static IRuleBuilderOptions<T, DateOnly?> ValidateDateTime<T>(this IRuleBuilder<T, DateOnly?> ruleBuilder)
        {
            return ruleBuilder
                       .Must(val =>
                       {
                           var key = DateOnly.TryParseExact(val?.ToString(), "d.M.yyyy", out DateOnly dateOnly);

                           return key;
                       }).WithMessage("У свойства {PropertyName} неправильно задана дата или её формат.");

        }
    }
}
