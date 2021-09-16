namespace ConsoleCanvas.Core
{
    public interface IDimension
    {
        Dimension2D CurrentDimension { get; }
        Dimension2D MaxDimension { get; }
    }
}