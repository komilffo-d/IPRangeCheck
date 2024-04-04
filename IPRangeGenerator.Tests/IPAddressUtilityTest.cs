using IPRangeGenerator.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPRangeGenerator.Tests
{
    public class IPAddressUtilityTest
    {
        [Theory]
        [InlineData("192.168.0.10")]
        [InlineData("192.168.1.200")]
        [InlineData("210.230.1.56")]
        public void IsValidIPv4_ValidIP_True(string ipAddress)
        {

            bool result = IPAddressUtility.IsValidIPv4(ipAddress);

            Assert.True( result);

        }

        [Theory]
        [InlineData("192.168.10")]
        [InlineData("192.392.5.1")]
        [InlineData("192.168.000.1")]
        [InlineData("192.28.2.010")]
        public void IsValidIPv4_ValidIP_False(string ipAddress)
        {

            bool result = IPAddressUtility.IsValidIPv4(ipAddress);

            Assert.False(result);

        }
    }
}
