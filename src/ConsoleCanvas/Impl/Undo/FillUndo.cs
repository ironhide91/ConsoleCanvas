using ConsoleCanvas.Core;
using ConsoleCanvas.Impl.Parameter;
using System.Collections.Generic;

namespace ConsoleCanvas.Impl.Undo
{
    public class FillUndo : IUndo<FillCommandParameter>
    {
        public void Undo(ICanvas2D canvas, FillCommandParameter param, IEnumerable<Point2D> previousCommandPoints)
        {
            // TODO
            Logger.Warn("RectUndo.Undo not implemented");
        }
    }
}