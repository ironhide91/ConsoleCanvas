using ConsoleCanvas.Core;
using ConsoleCanvas.Impl.Parameter;

namespace ConsoleCanvas.Impl.Executor
{
    public class FillCommandExecutor : CommandExecutorBase<FillCommandParameter>
    {
        public FillCommandExecutor(
            ICommandParser<FillCommandParameter> parser,
            ICommandValidator<FillCommandParameter> validator,
            IDrawCommand<FillCommandParameter> drawer,
            IUndo<FillCommandParameter> undo) : base(parser, validator, drawer, undo)
        {

        }

        public override string CommandKey { get { return Constants.FillKey; } }
    }
}