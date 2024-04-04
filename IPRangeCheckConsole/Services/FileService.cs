using IPRangeCheckConsole.Interfaces;

namespace IPRangeCheckConsole.Services
{
    internal class FileService : IFileReader, IFileWriter
    {
        public bool Read()
        {
            throw new NotImplementedException();
        }

        public bool Write(IEnumerable<object> values)
        {
            try
            {
                using var stream = new StreamWriter("ip_addresses.txt");
                foreach (var value in values)
                {
                    stream.WriteLine(value.ToString());
                }

                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
    }
}
