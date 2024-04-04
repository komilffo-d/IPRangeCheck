using IPRangeGenerator.Services;
using System.Net;

namespace IPRangeCheck.Tests
{
    public class IPRangeGeneratorTest
    {
        [Theory]
        [InlineData("192.168.0.10", "192.168.1.5", 1)]
        [InlineData("192.168.1.200", "192.168.0.5", -1)]
        [InlineData("192.168.0.10", "192.168.0.10", 0)]
        [InlineData("192.200.5.01", "192.178.89.100", -1)]
        [InlineData("210.230.1.56", "210.245.0.32", 1)]
        [InlineData("192.168.0.1", "192.168.0.10", 1)]
        public void IPAddressCompareTo(string ipAddressMin, string ipAddressMax, int resultExpected)
        {

            IPAddress entityIp = IPAddress.Parse(ipAddressMax);
            IPAddress IpAddressIn = IPAddress.Parse(ipAddressMin);

            entityIp.CompareTo(IpAddressIn, out int result);

            Assert.Equal<int>(resultExpected, result);

        }
    }
}
