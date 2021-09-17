using ConsoleCanvas.Core;
using ConsoleCanvas.Impl.Parameter;
using System.Collections.Generic;

namespace ConsoleCanvas.Impl.Undo
{
    public class RectUndo : IUndo<RectCommandParameter>
    {
        public void Undo(ICanvas2D canvas, RectCommandParameter param, IEnumerable<Point2D> previousCommandPoints)
        {
            // TODO
            Logger.Warn("RectUndo.Undo not implemented");
        }
    }
}