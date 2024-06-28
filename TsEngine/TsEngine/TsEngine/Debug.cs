using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TsEngine.TsEngine
{
    public class Debug
    {
        public static void Log(string msg = "LOG WAS EMPTY LOL :3")
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{msg}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Info(string msg = "LOG WAS EMPTY LOL :3")
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{msg}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Warning(string msg = "WARNING WAS EMPTY LOL :3")
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{msg}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Error(string msg = "ERROR WAS EMPTY LOL :3")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{msg}");
            Console.ForegroundColor = ConsoleColor.White;
        }


    }
}
