using ConsoleCanvas.Core;
using ConsoleCanvas.Impl.Parameter;
using ConsoleCanvas.Impl.Validator;
using ConsoleCanvas.Impl.DrawRoutine;
using Pidgin;
using static Pidgin.Parser;
using ConsoleCanvas.Impl.Undo;

namespace ConsoleCanvas.Impl.Parser
{
    public class LineCommandParser : ICommandParser<LineCommandParameter>
    {
        public bool TryParse(string parameter, out LineCommandParameter parsed)
        {
            var result = parser.Parse(parameter);

            parsed = result.Success ? result.Value : null;

            return result.Success;

            ICore core = null;
            core.LineParser().TryParse(null, out _);

        }

        private static readonly Parser<char, LineCommandParameter> parser =
            Map
            (
                (cc, p1, p2, _) => new LineCommandParameter(p1, p2 ),
                Common.CommandCharacterParser("L"),
                Common.Point2DParser,
                Common.Point2DParser,
                Not(AnyCharExcept(' '))
            );
    }

    
}