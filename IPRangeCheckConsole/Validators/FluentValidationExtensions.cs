using FluentValidation;
using System.Globalization;
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

        public static IRuleBuilderOptions<T, string> ValidateFilePathAddress<T>(this IRuleBuilder<T, string> ruleBuilder)
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

        public static IRuleBuilderOptions<T, DateTime?> ValidateDateTime<T>(this IRuleBuilder<T, DateTime?> ruleBuilder)
        {
            return ruleBuilder
                       .Must(val =>
                       {
                           return DateTime.TryParseExact(val.ToString(), "d.M.yyyy", null, DateTimeStyles.None, out DateTime dateTime);
                       }).WithMessage("У свойства {PropertyName} неправильно задана дата или её формат.");

        }
    }
}
