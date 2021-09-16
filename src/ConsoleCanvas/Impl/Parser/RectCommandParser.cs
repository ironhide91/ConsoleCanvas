using ConsoleCanvas.Core;
using ConsoleCanvas.Impl.Parameter;
using Pidgin;
using static Pidgin.Parser;

namespace ConsoleCanvas.Impl.Parser
{
    public class RectCommandParser : ICommandParser<RectCommandParameter>
    {
        public bool TryParse(string parameter, out RectCommandParameter parsed)
        {
            var result = parser.Parse(parameter);

            parsed = result.Success ? result.Value : null;

            return result.Success;
        }

        private static readonly Parser<char, RectCommandParameter> parser =
            Map
            (
                (cc, p1, p2, _) => new RectCommandParameter(p1, p2),
                Common.CommandCharacterParser("R"),
                Common.Point2DParser,
                Common.Point2DParser,
                Not(AnyCharExcept(' '))
            );
    }
}