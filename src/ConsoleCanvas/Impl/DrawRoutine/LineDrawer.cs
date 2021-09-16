using ConsoleCanvas.Core;
using ConsoleCanvas.Impl.Parameter;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ConsoleCanvas.Impl.DrawRoutine
{
    public class LineDrawer : IDrawCommand<LineCommandParameter>
    {
        public IEnumerable<DrawUnit> GetDrawUnits(IReadOnlyCanvas2D canvas, LineCommandParameter parameter)
        {
            var units = new Collection<DrawUnit>();
            parameter.DrawBresenham(canvas.DefaultDrawValue, units);
            return units;
        }
    }
}