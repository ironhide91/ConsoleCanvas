namespace ConsoleCanvas.Core
{
    public interface ICore : IDimension, IIntializeCanvas, ICanvas2D, IConsole
    {
        string CurrentCommand { get; }

        void Run();
    }

}