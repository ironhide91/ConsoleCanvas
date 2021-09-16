using ConsoleCanvas.Impl;
using System;

namespace ConsoleCanvas.Core
{
    public static class Logger
    {
        private static readonly IConsole Console = new ConsoleImpl();

        public static void Info(string line)
        {
            Record($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} INFO {line}");
        }

        public static void Debug(string line)
        {
            Record($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} DEBUG {line}");
        }

        public static void Warn(string line)
        {
            Record($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} WARN {line}", 6);
        }

        public static void Error(string line)
        {
            Record($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} ERROR {line}", 4);
        }

        private static void Record(string line, int fcolor = 7)
        {
            Console.WriteLine(line, fcolor);
        }
    }
}