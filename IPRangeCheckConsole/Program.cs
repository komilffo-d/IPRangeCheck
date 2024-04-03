using IPRangeGenerator;
using System.Net;
using System.Reflection.Emit;

namespace IPRangeCheckConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MainGenerator generator = new MainGenerator("192.168.0.0", "192.168.255.255");

            IEnumerable<IPAddress> ipAddresses= generator.GenerateEnumerableInRange(100);

            foreach (IPAddress IP in ipAddresses)
            {
                Console.WriteLine(IP.ToString());
            }
        }
    }
}
