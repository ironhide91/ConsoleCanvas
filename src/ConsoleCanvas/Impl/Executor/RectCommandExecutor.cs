using ConsoleCanvas.Core;
using ConsoleCanvas.Impl.Parameter;

namespace ConsoleCanvas.Impl.Executor
{
    public class RectCommandExecutor : CommandExecutorBase<RectCommandParameter>
    {
        public RectCommandExecutor(
            ICommandParser<RectCommandParameter> parser,
            ICommandValidator<RectCommandParameter> validator,
            IDrawCommand<RectCommandParameter> drawer,
            IUndo<RectCommandParameter> undo) : base(parser, validator, drawer, undo)
        {

        }

        public override string CommandKey { get { return Constants.RectKey; } }
    }
}