using IPRangeGenerator.Base;
using IPRangeGenerator.Exceptions;
using IPRangeGenerator.Interfaces;
using System.Net;
namespace IPRangeGenerator
{
    public class IPGenerator : BaseGenerator, IGenerator<IPAddress>
    {

        private const int COUNT_OCTET = 4;

        public override Random _random { get; init; }

        public IPAddress? MinValue { get; init; } = new IPAddress(new byte[] { byte.MinValue, byte.MinValue, byte.MinValue, byte.MinValue });
        public IPAddress? MaxValue { get; init; } = new IPAddress(new byte[] { byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue });


        public IPGenerator()
        {

        }

        public IPGenerator(byte[]? minIPAddress, byte[]? maxIPAddress) : base()
        {
            if (minIPAddress is not null && minIPAddress.Length != 4)
                throw new InvalidByteArraySizeException(4, minIPAddress.Length);

            if (maxIPAddress is not null && maxIPAddress.Length != 4)
                throw new InvalidByteArraySizeException(4, maxIPAddress.Length);


            MinValue = minIPAddress is not null ? new IPAddress(minIPAddress) : MinValue;
            MaxValue = maxIPAddress is not null ? new IPAddress(maxIPAddress) : MaxValue;
        }

        public IPGenerator(string? minIPAddress, string? maxIPAddress) : base()
        {
            if (!IPAddress.TryParse(minIPAddress, out IPAddress? tempMinValue))
                throw new InvalidDataException("Неправильный формат IP-адреса v4");

            if (!IPAddress.TryParse(maxIPAddress, out IPAddress? tempMaxValue))
                throw new InvalidDataException("Неправильный формат IP-адреса v4");


            MinValue = tempMinValue;
            MaxValue = tempMaxValue;
        }
        public IPAddress Generate()
        {

            Span<byte> ipBytes = stackalloc byte[4];
            _random.NextBytes(ipBytes);
            return new IPAddress(ipBytes);

        }

        public IPAddress GenerateInRange(bool isInclusive = false)
        {
            byte[] minBytes = MinValue!.GetAddressBytes(), maxBytes = MaxValue!.GetAddressBytes();
            Span<byte> ipBytes = stackalloc byte[4];
            _random.NextBytes(ipBytes);

            for (int i = 0; i < COUNT_OCTET; i++)
            {
                if (isInclusive)
                {
                    ipBytes[i] = Math.Max(Math.Min(ipBytes[i], maxBytes[i] > 0 ? maxBytes[i]++ : maxBytes[i]), minBytes[i] < 255 ? minBytes[i]-- : minBytes[i]);
                }
                else
                {
                    ipBytes[i] = Math.Max(Math.Min(ipBytes[i], maxBytes[i]), minBytes[i]);
                }


            }
            return new IPAddress(ipBytes);


        }

        public IEnumerable<IPAddress> GenerateEnumerable(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return Generate();
            }

        }

        public IEnumerable<IPAddress> GenerateEnumerableInRange(int count, bool isInclusive = false)
        {
            {
                for (int i = 0; i < count; i++)
                {
                    yield return GenerateInRange(isInclusive);
                }
            }
        }

    }
}
