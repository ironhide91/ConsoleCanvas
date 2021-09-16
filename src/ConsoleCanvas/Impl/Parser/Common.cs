using ConsoleCanvas.Core;
using Pidgin;
using System;
using static Pidgin.Parser;

namespace ConsoleCanvas.Impl.Parser
{
    public static class Common
    {
        public static readonly Func<string, Parser<char, string>> CommandCharacterParser =
            command =>
                Map
                (
                    (ws1, cc, ws2) => cc,
                    Whitespaces,
                    CIString(command),
                    Whitespaces
                );

        public static readonly Parser<char, Point2D> Point2DParser =
            Map
            (
                (x1, ws1, y1, ws2) => new Point2D(x1, y1),
                Int(10),
                Whitespaces,
                Int(10),
                Whitespaces
            );
    }
}
