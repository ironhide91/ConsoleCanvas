using ConsoleCanvas.Core;
using System.Collections.Generic;

namespace ConsoleCanvas.Impl.Parameter
{
    public class LineCommandParameter : IDrawRoutineParam
    {
        public LineCommandParameter(Point2D point1, Point2D point2)
        {
            Point1 = point1;
            Point2 = point2;
        }

        public readonly Point2D Point1;
        public readonly Point2D Point2;

        public string CommandKey { get { return Constants.LineKey; } }
        public IEnumerable<Point2D> AssociatedPoints { get; set; }
        public int BackgroundColorBeforeFill { get; set; }

        public override string ToString()
        {
            return $"[Command {CommandKey} | Point1 {Point1} | Point2 {Point2}]";
        }
    }
}