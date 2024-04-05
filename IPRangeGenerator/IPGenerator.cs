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

            if (!IPAddressUtility.IsValidIPv4(maxIPAddress) || !IPAddress.TryParse(maxIPAddress, out IPAddress? tempMaxValue))
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

        public IPAddress GenerateInRange(bool isInclusive = true)
        {
            byte[] minBytes = MinValue!.GetAddressBytes(), maxBytes = MaxValue!.GetAddressBytes();
            Span<byte> ipBytes = new byte[4];
            _random.NextBytes(ipBytes);

            for (int i = 0; i < COUNT_OCTET; i++)
                ipBytes[i] = RandomNext(minValue: minBytes[i], maxValue: maxBytes[i], isInclusive);
            return new IPAddress(ipBytes);


        }

        public IEnumerable<IPAddress> GenerateEnumerable(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return Generate();
            }

        }

        public IEnumerable<IPAddress> GenerateEnumerableInRange(int count, bool isInclusive = true)
        {
            {
                for (int i = 0; i < count; i++)
                {
                    yield return GenerateInRange(isInclusive);
                }
            }
        }

        private protected byte RandomNext(byte minValue, byte maxValue, bool isInclusive = true)
        {
            if (isInclusive)
            {
                if (minValue <= maxValue)
                    return (byte)_random.Next(minValue,  (int)maxValue  + 1);
                else
                    return (byte)_random.Next(maxValue, (int)minValue + 1);
            }
            else
            {
                if (minValue <= maxValue)
                    return (byte)_random.Next(minValue, maxValue);
                else
                    return (byte)_random.Next(maxValue, minValue);
            }

        }

    }
}
