namespace ConsoleCanvas.Core
{
    public interface IRender
    {
        void Render(IReadOnlyCanvas2D canvas, IConsole console);
    }
}