using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace IPRangeCheckConsole.Attributes
{
    internal class DateTimeValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            return DateTime.TryParseExact(value.ToString(), "d.M.yyyy", null, DateTimeStyles.None, out DateTime dateTime);
        }
    }
}
