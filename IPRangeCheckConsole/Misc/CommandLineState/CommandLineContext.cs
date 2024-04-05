namespace IPRangeCheckConsole.Misc.CommandLineState
{
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


        public async Task ArgumentProcessAsync(string[]? args = null)
        {
            await _stateCLI.ArgumentProcessAsync(args);
        }
    }
}
