using ConsoleCanvas.Core;
using ConsoleCanvas.Impl.Parameter;
using Pidgin;
using static Pidgin.Parser;

namespace ConsoleCanvas.Impl.Parser
{
    public class CanvasCommandParser : ICommandParser<CanvasCommandParameter>
    {
        public bool TryParse(string parameter, out CanvasCommandParameter parsed)
        {
            var result = parser.Parse(parameter);

            parsed = result.Success ? result.Value : null;

            return result.Success;
        }

        private static readonly Parser<char, CanvasCommandParameter> parser =
            Map(
                (cc, w, _, h, _, _) =>
                {
                    return new CanvasCommandParameter(new Dimension2D(w, h));
                },
                Common.CommandCharacterParser("C"),
                Int(10),
                Whitespaces,
                Int(10),
                Whitespaces,
                Not(AnyCharExcept(' '))
            );
    }
}