namespace IPRangeCheckConsole.Misc.CommandLineState
{
    public abstract class ArgumentState
    {

        private protected CommandLineContext _context;

        public void SetContext(CommandLineContext context)
        {
            _context = context;
        }
        public abstract Task<bool> ArgumentProcessAsync(string[]? args);
    }
}
