using System.Net;

namespace IPRangeCheckConsole.Services
{
    public static class IPAddressExtension
    {
        public static void CompareTo(this IPAddress IpAddress, in IPAddress IpAddressIn, out int result)
        {

            Stack<int> stackCompare = new Stack<int>();
            byte[] bytesIpAddress = IpAddress.GetAddressBytes(), bytesIpAddressIn = IpAddressIn.GetAddressBytes();

            for (int i = 0; i < bytesIpAddress.Length; i++)
            {
                if (bytesIpAddress[i] > bytesIpAddressIn[i])
                    stackCompare.Push(1);
                else if (bytesIpAddress[i] < bytesIpAddressIn[i])
                {

                    stackCompare.Push(-1);
                }   


            }
            
        }
    }
}
