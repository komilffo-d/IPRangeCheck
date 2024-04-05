using IPRangeGenerator;

namespace IPRangeCheckConsole.Services
{
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
        public void 
        private IPGenerator _IPGenerator { get; set; }
        private DateTimeGenerator _DateTimeGenerator { get;  set; }

    }
}
