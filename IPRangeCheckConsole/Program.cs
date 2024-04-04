using IPRangeGenerator;
using System.Globalization;
using System.Net;

namespace IPRangeCheckConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {

            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ru-RU");

            IPGenerator generator;
            DateTimeGenerator generator2;
            try
            {
                generator = new IPGenerator("100.10.0.0", "192.168.255.255");

                generator2 = new DateTimeGenerator("2.4.2001 6:8:20", "6.4.2001 12:45:30");

                IEnumerable<IPAddress> ipAddresses = generator.GenerateEnumerableInRange(100);

                IEnumerable<DateTime> dateTimes = generator2.GenerateEnumerableInRange(100,true);

                var collection = ipAddresses.Zip(dateTimes);

                foreach (var item in collection)
                {
                    Console.WriteLine(string.Format("{0, 20}  ||  {1,30}", item.First,item.Second));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Console.WriteLine("\n\nПрограмма завершена!");
            }



        }
    }
}
