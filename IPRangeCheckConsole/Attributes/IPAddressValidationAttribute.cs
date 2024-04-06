using System.ComponentModel.DataAnnotations;
using System.Net;

namespace IPRangeCheckConsole.Attributes
{
    internal class IPAddressValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string? ipAddressString = value as string;

            return IPAddress.TryParse(ipAddressString, out IPAddress ipAddress);
        }
    }
}
