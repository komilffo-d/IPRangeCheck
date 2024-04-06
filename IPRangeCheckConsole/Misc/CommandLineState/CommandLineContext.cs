namespace IPRangeCheckConsole.Misc.CommandLineState
{
    //Паттерн "Состяние"
    public class CommandLineContext
    {
        private ArgumentState _stateCLI;

        public CommandLineContext(ArgumentState stateCLI)
        {
            SwitchToState(stateCLI);
        }

        public void SwitchToState(ArgumentState stateCLI)
        {
            Console.WriteLine("Состояние изменено!");
            _stateCLI = stateCLI;
            _stateCLI.SetContext(this);
        }

        //Возвращаю именно return await, а не просто return Task<T>, чтобы было перехвачено исключение в блоке try/catch.
        public async Task<bool> ArgumentProcessAsync(string[]? args = null)
        {
            return await _stateCLI.ArgumentProcessAsync(args);
        }
    }
}
