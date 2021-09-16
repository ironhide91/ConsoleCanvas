using ConsoleCanvas.Core;
using ConsoleCanvas.Impl.Parameter;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ConsoleCanvas.Impl.DrawRoutine
{
    public class RectDrawer : IDrawCommand<RectCommandParameter>
    {
        public IEnumerable<DrawUnit> GetDrawUnits(IReadOnlyCanvas2D canvas, RectCommandParameter parameter)
        {
            var units = new Collection<DrawUnit>();

            var upperRightPoint = new Point2D(parameter.LowerRightPoint.X, parameter.UpperLeftPoint.Y);
            var lowerLeftPoint = new Point2D(parameter.UpperLeftPoint.X, parameter.LowerRightPoint.Y);

            LineDrawerExtensions.DrawHorizontal(parameter.UpperLeftPoint, upperRightPoint, canvas.DefaultDrawValue, units);
            LineDrawerExtensions.DrawHorizontal(lowerLeftPoint, parameter.LowerRightPoint, canvas.DefaultDrawValue, units);

            LineDrawerExtensions.DrawVertical(parameter.UpperLeftPoint, lowerLeftPoint, canvas.DefaultDrawValue, units);
            LineDrawerExtensions.DrawVertical(upperRightPoint, parameter.LowerRightPoint, canvas.DefaultDrawValue, units);

            return units;
        }
    }
}