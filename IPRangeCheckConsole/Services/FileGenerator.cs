using IPRangeCheckConsole.Interfaces;
using IPRangeGenerator;
using System.Net;

namespace IPRangeCheckConsole.Services
{
    /// <summary>
    /// Да-да, это потокобезопасный Singleton :(
    /// </summary>
    internal sealed class FileGenerator
    {
        private FileGenerator() { }

        private static FileGenerator? _entity;

        private static readonly object _lock = new object();

        public static bool CreateEntity()
        {
            bool isCreated = false;
            if (_entity == null)
            {
                lock (_lock)
                {
                    if (_entity == null)
                    {
                        _entity = new FileGenerator();
                        isCreated = true;
                    }
                }
            }
            return isCreated;
        }
        public static FileGenerator? GetEntity() => _entity;

        public void SetIPGenerator(IPGenerator generator)
        {
            _IPGenerator = generator;
        }
        public void SetDateTimeGenerator(DateTimeGenerator generator)
        {
            _DateTimeGenerator = generator;
        }
        public async Task WriteFileAsync(string pathFile, int countEntries, bool isInclusive)
        {
            if (_IPGenerator is null || _DateTimeGenerator is null)
                throw new InvalidOperationException("Не назначены генераторы IP-адрессов и дат!");

            IEnumerable<IPAddress> ipAddresses = _IPGenerator.GenerateEnumerableInRange(countEntries, isInclusive);

            IEnumerable<DateTime> dateTimes = _DateTimeGenerator.GenerateEnumerableInRange(countEntries, isInclusive);

            IEnumerable<string> collection = ipAddresses.Zip(dateTimes, (ip, dateTime) => (IP: ip, DateTime: dateTime))
                                                        .Select(t => $"{t.IP};{t.DateTime.ToString("yyyy-MM-dd HH:mm:ss")}");

            await _fileService.Value.WriteAsync(pathFile, collection);
        }

        private readonly Lazy<IFileWriter> _fileService = new Lazy<IFileWriter>(() => new FileService());
        private IPGenerator? _IPGenerator { get; set; }
        private DateTimeGenerator? _DateTimeGenerator { get; set; }

    }
}
