using ConsoleCanvas.Core;
using System.Collections.Generic;

namespace ConsoleCanvas.Impl.Parameter
{
    public class RectCommandParameter : IDrawRoutineParam
    {
        public RectCommandParameter(Point2D upperLeftPoint, Point2D lowerRightPoint)
        {
            UpperLeftPoint = upperLeftPoint;
            LowerRightPoint = lowerRightPoint;
        }

        public readonly Point2D UpperLeftPoint;
        public readonly Point2D LowerRightPoint;

        public string CommandKey { get { return Constants.RectKey; } }
        public IEnumerable<Point2D> AssociatedPoints { get; set; }
        public int BackgroundColorBeforeFill { get; set; }

        public override string ToString()
        {
            return $"[Command {CommandKey} | UpperLeftPoint {UpperLeftPoint} | LowerRightPoint {LowerRightPoint}]";
        }
    }
}