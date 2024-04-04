using IPRangeGenerator.Misc;
using IPRangeGenerator.Services;
using System.Net;

namespace IPRangeGenerator.Tests
{
    public class IPAddressExtensionTest
    {
        [Theory]
        [InlineData("192.168.0.10", "192.168.1.5")]
        [InlineData("210.230.1.56", "210.245.0.32")]
        [InlineData("192.168.0.1", "192.168.0.10")]
        public void CompareTo_MinIpCompareMaxIp_EqualOne(string ipAddressMin, string ipAddressMax)
        {
            
            IPAddress entityIp = IPAddress.Parse(ipAddressMax);
            IPAddress IpAddressIn = IPAddress.Parse(ipAddressMin);
            
            entityIp.CompareTo(IpAddressIn, out int result);

            Assert.Equal(1, result);

        }

        [Theory]
        [InlineData("192.168.0.10", "192.168.0.10")]
        [InlineData("192.28.2.10", "192.28.2.10")]
        [InlineData("253.43.0.0", "253.43.0.0")]
        [InlineData("0.0.0.0", "0.0.0.0")]
        [InlineData("255.255.255.255", "255.255.255.255")]
        public void CompareTo_MinIpCompareMaxIp_EqualZero(string ipAddressMin, string ipAddressMax)
        {

            IPAddress entityIp = IPAddress.Parse(ipAddressMax);
            IPAddress IpAddressIn = IPAddress.Parse(ipAddressMin);

            entityIp.CompareTo(IpAddressIn, out int result);

            Assert.Equal(0, result);

        }

        [Theory]
        [InlineData("192.168.1.200", "192.168.0.5")]
        [InlineData("192.200.5.01", "192.178.89.100")]
        [InlineData("0.0.54.1", "0.0.52.255")]
        public void CompareTo_MinIpCompareMaxIp_EqualNegativeOne(string ipAddressMin, string ipAddressMax)
        {

            IPAddress entityIp = IPAddress.Parse(ipAddressMax);
            IPAddress IpAddressIn = IPAddress.Parse(ipAddressMin);

            entityIp.CompareTo(IpAddressIn, out int result);

            Assert.Equal(-1, result);

        }
    }
}
