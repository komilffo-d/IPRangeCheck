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
            using StreamReader reader = new StreamReader(filePath);


            string line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                yield return line;
            }

        }

        public async void WriteAsync(string filePath, IEnumerable<string> values)
        {

            using var writer = new StreamWriter(filePath);

            foreach (string line in values)
            {
                await writer.WriteLineAsync(line);
            }



        }
    }
}
