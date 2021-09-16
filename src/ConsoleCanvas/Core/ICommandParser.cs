namespace ConsoleCanvas.Core
{
    public interface ICommandParser<T> where T : IDrawRoutineParam
    {
        bool TryParse(string parameter, out T parsed);
    }
}