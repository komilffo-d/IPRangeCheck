using IPRangeCheckConsole.Interfaces;

namespace IPRangeCheckConsole.Services
{
    public class FileService : IFileReader, IFileWriter
    {
        public async IAsyncEnumerable<string> ReadAsync(string filePath)
        {
            if (!File.Exists(filePath))
                throw new InvalidDataException($"Передан несуществующий путь к файлу! | {nameof(ReadAsync)}");
            using StreamReader reader = new StreamReader(filePath);


            string line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                yield return line;
            }

        }

        public async Task WriteAsync(string filePath, IEnumerable<string> values)
        {

            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                throw new InvalidDataException($"Передана не существующая директория! | {nameof(WriteAsync)}");
            // await для очистки буффера
            await using (var writer = new StreamWriter(filePath, false))
            {
                foreach (string line in values)
                {
                    await writer.WriteLineAsync(line);
                }
            }


        }
    }
}
