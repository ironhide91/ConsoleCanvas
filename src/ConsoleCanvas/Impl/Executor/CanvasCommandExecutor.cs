using ConsoleCanvas.Core;
using ConsoleCanvas.Impl.Parameter;
using System;

namespace ConsoleCanvas.Impl.Executor
{
    public sealed class CanvasCommandExecutor : IDrawCanvasCommand
    {
        public CanvasCommandExecutor(
            ICommandParser<CanvasCommandParameter> parser,
            ICommandValidator<CanvasCommandParameter> validator)
        {
            if (parser == null)
                throw new ArgumentNullException();

            if (validator == null)
                throw new ArgumentNullException();

            this.parser = parser;
            this.validator = validator;
        }

        private readonly ICommandParser<CanvasCommandParameter> parser;
        private readonly ICommandValidator<CanvasCommandParameter> validator;

        public string CommandKey { get { return Constants.CanvasKey; } }
        public ICommandParser<CanvasCommandParameter> Parser { get { return parser; } }
        public ICommandValidator<CanvasCommandParameter> Validator { get { return validator; } }

        public void Draw(IIntializeCanvas initializer, Dimension2D dimension)
        {
            initializer.Initialize(dimension);
        }
    }
}