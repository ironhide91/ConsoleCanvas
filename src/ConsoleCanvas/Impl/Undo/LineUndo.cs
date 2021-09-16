using ConsoleCanvas.Core;
using ConsoleCanvas.Impl.Parameter;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleCanvas.Impl.Undo
{
    public class LineUndo : IUndo<LineCommandParameter>
    {
        public void Undo(ICanvas2D canvas, LineCommandParameter param, IEnumerable<Point2D> previousCommandPoints)
        {
            var set = canvas.Get();
            var hasOverlapWithPrevious = previousCommandPoints != null;
            var previousPoints = hasOverlapWithPrevious ? previousCommandPoints.Distinct().ToHashSet() : null;

            foreach (var point in param.AssociatedPoints)
            {
                if (set.ContainsKey(point))
                {
                    if (hasOverlapWithPrevious
                        && previousPoints.Contains(point)
                        && set[point].Value != canvas.DefaultDrawValue)
                    {
                        continue;
                    }

                    set[point].Value = canvas.EmptyDrawValue;
                    set[point].BackgroundColor = canvas.DefaultBackgroundColor;
                    set[point].ForegroundColor = canvas.DefaultForegroundColor;
                }
            }
        }
    }
}