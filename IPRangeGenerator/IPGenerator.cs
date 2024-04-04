using IPRangeGenerator.Base;
using IPRangeGenerator.Exceptions;
using IPRangeGenerator.Interfaces;
using IPRangeGenerator.Misc;
using IPRangeGenerator.Services;
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
        public IPGenerator(byte[] minIPAddress, byte[] maxIPAddress) : base()
        {
            if (minIPAddress is null || minIPAddress.Length != 4)
                throw new InvalidByteArraySizeException(4, minIPAddress.Length);

            if (maxIPAddress is null || maxIPAddress.Length != 4)
                throw new InvalidByteArraySizeException(4, maxIPAddress.Length);

            IPAddress tempMinValue = new IPAddress(minIPAddress), tempMaxValue = new IPAddress(maxIPAddress);
            tempMaxValue.CompareTo(tempMinValue, out int result);
            if (result < 0)
                throw new InvalidDataException("Нижняя граница IP-зоны выше верхней");

            MinValue = minIPAddress is not null ? new IPAddress(minIPAddress) : MinValue;
            MaxValue = maxIPAddress is not null ? new IPAddress(maxIPAddress) : MaxValue;
        }

        public IPGenerator(string minIPAddress, string maxIPAddress) : base()
        {
            if (!IPAddressUtility.IsValidIPv4(minIPAddress) || !IPAddress.TryParse(minIPAddress, out IPAddress? tempMinValue))
                throw new InvalidDataException("Неправильный формат IP-адреса v4");

            if (!IPAddressUtility.IsValidIPv4(maxIPAddress) ||  !IPAddress.TryParse(maxIPAddress, out IPAddress? tempMaxValue))
                throw new InvalidDataException("Неправильный формат IP-адреса v4");
            tempMaxValue.CompareTo(tempMinValue, out int result);
            if (result < 0)
                throw new InvalidDataException("Нижняя граница IP-зоны выше верхней");


            MinValue = tempMinValue;
            MaxValue = tempMaxValue;
        }
        public IPAddress Generate()
        {

            Span<byte> ipBytes = new byte[4];
            _random.NextBytes(ipBytes);
            return new IPAddress(ipBytes);

        }

        public IPAddress GenerateInRange(bool isInclusive = false)
        {
            byte[] minBytes = MinValue!.GetAddressBytes(), maxBytes = MaxValue!.GetAddressBytes();
            Span<byte> ipBytes = new byte[4];
            _random.NextBytes(ipBytes);

            for (int i = 0; i < COUNT_OCTET; i++)
            {
                if (isInclusive)
                {
                    ipBytes[i] = (byte)_random.Next(minBytes[i] < 255 ? minBytes[i]-- : minBytes[i], maxBytes[i] > 0 ? maxBytes[i] : maxBytes[i] + 1);
                }
                else
                {
                    ipBytes[i] = (byte)_random.Next(minBytes[i], maxBytes[i] + 1);
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
