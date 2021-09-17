using System.Collections.Generic;

namespace ConsoleCanvas.Core
{
    public interface IReadOnlyCanvas2D
    {
        char EmptyDrawValue { get; }
        char DefaultDrawValue { get; }
        int DefaultBackgroundColor { get; }
        int DefaultForegroundColor { get; }
        Dimension2D CurrentDimension { get; }
        IReadOnlyDictionary<Point2D, DrawUnit> Get();
    }
}