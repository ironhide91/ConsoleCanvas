namespace ConsoleCanvas.Core
{
    public interface ICommandValidator<T> where T : IDrawRoutineParam
    {
        bool IsValid(T parameter, Dimension2D canvasDimension);
    }
}