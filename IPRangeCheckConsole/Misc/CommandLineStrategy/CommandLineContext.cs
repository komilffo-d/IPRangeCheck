namespace IPRangeCheckConsole.Misc.CommandLineState
{
    //Паттерн "Стратегия"
    public class CommandLineContext
    {
        private ArgumentStrategy _strategyCLI;


        public CommandLineContext()
        {

        }

        public CommandLineContext(ArgumentStrategy strategyCLI)
        {
            SwitchToStrategy(strategyCLI);
        }

        public void SwitchToStrategy(ArgumentStrategy strategyCLI)
        {

            _strategyCLI = strategyCLI;
        }

        //Возвращаю именно return await Task<T>, а не просто return Task<T>, чтобы было перехвачено исключение в блоке try/catch.
        public async Task<bool> ArgumentProcessAsync(string[]? args = null)
        {
            return await _strategyCLI.ArgumentProcessAsync(args);
        }
    }
}
