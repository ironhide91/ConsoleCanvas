using ConsoleCanvas.Core;
using System;

namespace ConsoleCanvas.Impl
{
    public class ConsoleImpl : IConsole
    {
        public ConsoleColor DefaultBackgroundColor { get { return ConsoleColor.Black; } }

        public ConsoleColor DefaultForegroundColor { get { return ConsoleColor.Gray; } }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void NewLine()
        {
            Console.WriteLine();
        }        

        public void Write(string value)
        {
            Console.Write(value);
        }

        public void WriteLine(string value)
        {
            Console.WriteLine(value);
        }

        public void Write(DrawUnit unit)
        {
            Console.BackgroundColor = (ConsoleColor)unit.BackgroundColor;
            Console.Write(unit.Value);
            Console.BackgroundColor = DefaultBackgroundColor;
        }

        public void WriteLine(string value, int fcolor)
        {
            if (fcolor >= 0 && fcolor <= 15)
                Console.ForegroundColor = (ConsoleColor)fcolor;

            Console.WriteLine(value);
        }
    }
}