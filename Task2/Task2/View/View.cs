using System;

namespace Task2
{
    public abstract class View
    {
        public static void ConsoleWriteLine(string text)
        {
            Console.WriteLine(text);
        }
        public static void ConsoleWriteLine(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
