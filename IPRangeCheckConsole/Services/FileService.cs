using IPRangeCheckConsole.Interfaces;

namespace IPRangeCheckConsole.Services
{
    internal class FileService : IFileReader, IFileWriter
    {
        public bool Read(string filePath)
        {
            throw new NotImplementedException();
        }

        public async IAsyncEnumerable<string> ReadAsync(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    yield return line;
                }
            }
        }

        public bool Write(IEnumerable<string> values)
        {
            try
            {
                using var stream = new StreamWriter("ip_addresses.txt");
                foreach (string? value in values)
                    stream.WriteLine(value.ToString());

                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
    }
}
