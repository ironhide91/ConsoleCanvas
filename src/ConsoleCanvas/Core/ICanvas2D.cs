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

    public interface IMutableCanvas2D
    {
        bool Initialize(Dimension2D dimension);
        void Draw(DrawUnit unit);
        void Draw(IEnumerable<DrawUnit> units);
        void Reset();
    }

    public interface ICanvas2D : IReadOnlyCanvas2D, IMutableCanvas2D
    {

    }
}