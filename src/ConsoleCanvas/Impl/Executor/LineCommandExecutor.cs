using ConsoleCanvas.Core;
using ConsoleCanvas.Impl.Parameter;

namespace ConsoleCanvas.Impl.Executor
{
    public class LineCommandExecutor : CommandExecutorBase<LineCommandParameter>
    {
        public LineCommandExecutor(
            ICommandParser<LineCommandParameter> parser,
            ICommandValidator<LineCommandParameter> validator,
            IDrawCommand<LineCommandParameter> drawer,
            IUndo<LineCommandParameter> undo) : base(parser, validator, drawer, undo)
        {

        }

        public override string CommandKey { get { return Constants.LineKey; } }
    }
}