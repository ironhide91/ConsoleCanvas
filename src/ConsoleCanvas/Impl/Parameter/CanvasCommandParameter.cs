using ConsoleCanvas.Core;
using System.Collections.Generic;

namespace ConsoleCanvas.Impl.Parameter
{
    public class CanvasCommandParameter : IDrawRoutineParam
    {
        public CanvasCommandParameter(Dimension2D dimension)
        {
            Dimension = dimension;
        }

        public readonly Dimension2D Dimension;

        public string CommandKey { get { return Constants.CanvasKey; } }
        public IEnumerable<Point2D> AssociatedPoints { get; set; }
        public int BackgroundColorBeforeFill { get; set; }

        public override string ToString()
        {
            return $"[Width {Dimension.Width} | Height {Dimension.Height}]";
        }
    }
}