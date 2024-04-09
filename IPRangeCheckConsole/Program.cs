using IPRangeCheckConsole.Facade;
using IPRangeCheckConsole.Services;
using Serilog;

namespace IPRangeCheckConsole
{
    internal sealed class Program
    {
        static async Task Main(string[] args)
        {
            InitialSubSystem initialSubSystem = InitialSubSystem.CreateInstance();

            // Класс "Фасад" приложения, в котором инкапсулируется логига подсистем
            ApplicationFacade applicationFacade = new ApplicationFacade(initialSubSystem);

            int code=await applicationFacade.RunAsync(args);

            switch(code)
            {
                case 1:
                    Log.Information("Программа успешно выполнила свою функцию!");
                    break;
                case 0:
                    Log.Information("Программа не выполнила свою функцию из-за неверных входных данных!");
                    break;
                case -1:
                    Log.Information("Сработала критическая ошибка. Попробуйте снова!");
                    break;
            }


            ConsoleService.ConsoleWriteLineCenter("PRESS KEY");
            Console.ReadKey();
            Environment.Exit(code);
        }

    }
}
