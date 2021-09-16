using ConsoleCanvas.Core;
using ConsoleCanvas.Impl.Parameter;
using Pidgin;
using System;
using System.Linq;
using static Pidgin.Parser;

namespace ConsoleCanvas.Impl.Parser
{
    public class FillCommandParser : ICommandParser<FillCommandParameter>
    {
        public bool TryParse(string parameter, out FillCommandParameter parsed)
        {
            var result = parser.Parse(parameter);

            parsed = result.Success ? result.Value : null;

            return result.Success;
        }

        private static readonly Parser<char, ConsoleColor> colorParser =
            Map
            (
                color => (ConsoleColor)color,
                OneOf(Enumerable.Range(0, 16).Select(i => (char)i))
            );

        private static readonly Parser<char, FillCommandParameter> parser =
            Map
            (
                (cc, point, color) => new FillCommandParameter(point, color),
                Common.CommandCharacterParser("B"),
                Common.Point2DParser,
                Int(10)
            );
    }
}