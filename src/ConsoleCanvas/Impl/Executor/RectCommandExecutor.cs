using ConsoleCanvas.Core;
using ConsoleCanvas.Impl.Parameter;

namespace ConsoleCanvas.Impl.Executor
{
    public class RectCommandExecutor : CommandExecutorBase<RectCommandParameter>
    {
        public RectCommandExecutor(
            ICommandParser<RectCommandParameter> parser,
            ICommandValidator<RectCommandParameter> validator,
            IDrawCommand<RectCommandParameter> drawer) : base(parser, validator, drawer)
        {

        }

        public override string CommandKey { get { return Constants.RectKey; } }
    }
}