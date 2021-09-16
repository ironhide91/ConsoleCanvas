using System;

namespace ConsoleCanvas.Core
{
    public interface IConsole
    {
        ConsoleColor DefaultBackgroundColor { get; }

        ConsoleColor DefaultForegroundColor { get; }

        string ReadLine();

        void Write(string value);

        void WriteLine(string value);

        void WriteLine(string value, int fcolor);

        void Write(DrawUnit unit);

        void NewLine();
    }
}