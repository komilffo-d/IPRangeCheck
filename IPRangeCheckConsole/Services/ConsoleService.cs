using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPRangeCheckConsole.Services
{
    internal static class ConsoleService
    {
        public static void ConsoleWriteLineCenter(string message)
        {
            Console.WriteLine(new string('-', Console.WindowWidth));
            Console.SetCursorPosition((Console.WindowWidth - message.Length) / 2, Console.CursorTop);
            Console.WriteLine(message);
            Console.WriteLine(new string('-', Console.WindowWidth));
        }
    }
}
