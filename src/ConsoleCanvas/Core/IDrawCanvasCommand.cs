using ConsoleCanvas.Impl.Parameter;

namespace ConsoleCanvas.Core
{
    public interface IDrawCanvasCommand : ICommandKey
    {
        ICommandParser<CanvasCommandParameter> Parser { get; }

        ICommandValidator<CanvasCommandParameter> Validator { get; }

        void Draw(IIntializeCanvas initializer, Dimension2D dimension);
    }
}