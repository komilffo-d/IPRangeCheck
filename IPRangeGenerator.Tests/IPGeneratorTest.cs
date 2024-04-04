using IPRangeGenerator.Exceptions;

namespace IPRangeGenerator.Tests
{
    public class IPGeneratorTest
    {

        [Theory]
        [InlineData(new byte[] { 192, 175, 68, 45 }, new byte[] { 127, 3 })]
        [InlineData(new byte[] { 1 }, new byte[] { 0 })]
        public void Constructor_CreateEntityByByte_InvalidByteArraySizeException(byte[] minIPAddress, byte[] maxIPAddress)
        {

            Assert.Throws<InvalidByteArraySizeException>(() => new IPGenerator(minIPAddress, maxIPAddress));
        }

        [Theory]
        [InlineData(new byte[] { 192, 175, 68, 45 }, new byte[] { 127, 3, 8, 10 })]
        [InlineData(new byte[] { 192, 168, 5, 250 }, new byte[] { 192, 168, 0, 252 })]
        public void Constructor_CreateEntityByByte_InvalidDataException(byte[] minIPAddress, byte[] maxIPAddress)
        {

            Assert.Throws<InvalidDataException>(() => new IPGenerator(minIPAddress, maxIPAddress));
        }

        [Theory]
        
        [InlineData("192.175.68.45", "127.3")]
        [InlineData("127.3", "192.175.68.45")]
        [InlineData("192.168.5.250","192.168.0.252")]
        public void Constructor_CreateEntityByString_InvalidDataException(string minIPAddress, string maxIPAddress)
        {
            
            Assert.Throws<InvalidDataException>(() => new IPGenerator(minIPAddress, maxIPAddress));
        }

    }
}
