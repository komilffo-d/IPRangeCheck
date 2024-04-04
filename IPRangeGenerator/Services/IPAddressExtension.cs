using System.Net;

namespace IPRangeGenerator.Services
{
    public static class IPAddressExtension
    {
        public static uint ToUint32(this IPAddress ipAddress)
        {
            var bytes = ipAddress.GetAddressBytes();

            return ((uint)(bytes[0] << 24)) |
                   ((uint)(bytes[1] << 16)) |
                   ((uint)(bytes[2] << 8)) |
                   ((uint)(bytes[3]));
        }
        public static void CompareTo(this IPAddress IpAddress, in IPAddress IpAddressIn, out int result)
        {

            uint uintIpAddress = IpAddress.ToUint32(), uintIpAddressIn = IpAddressIn.ToUint32();
            if (uintIpAddress == uintIpAddressIn)
                result = 0;
            else if (uintIpAddress > uintIpAddressIn)
                result = 1;

            else
                result = -1;

        }

    }
}
