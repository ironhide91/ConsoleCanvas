using ConsoleCanvas.Core;
using System.Collections.Generic;

namespace ConsoleCanvas.Impl.Parameter
{
    public class FillCommandParameter : IDrawRoutineParam
    {
        public FillCommandParameter(Point2D point, int fillColor)
        {
            Point = point;
            FillColor = fillColor;
        }

        public readonly Point2D Point;
        public readonly int FillColor;

        public string CommandKey { get { return Constants.FillKey; } }
        public IEnumerable<Point2D> AssociatedPoints { get; set; }
        public int BackgroundColorBeforeFill { get; set; }
    }
}