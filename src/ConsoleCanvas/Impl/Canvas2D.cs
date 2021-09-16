using ConsoleCanvas.Core;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleCanvas.Impl
{
    public class Canvas2D : ICanvas2D
    {
        public Canvas2D(
            char emptyDrawValue,
            char defaultDrawValue,
            int defaultBackgroundColor,
            int defaultForegroundColor,
            IComparer<Point2D> comparer)
        {
            this.emptyDrawValue = emptyDrawValue;
            this.defaultDrawValue = defaultDrawValue;
            this.defaultBackgroundColor = defaultBackgroundColor;
            this.defaultForegroundColor = defaultForegroundColor;
            canvas = new SortedDictionary<Point2D, DrawUnit>(comparer);
        }

        private readonly SortedDictionary<Point2D, DrawUnit> canvas;
        private readonly char emptyDrawValue;
        private readonly char defaultDrawValue;
        private readonly int defaultBackgroundColor;
        private readonly int defaultForegroundColor;
        private Dimension2D currentDimension;

        public Dimension2D CurrentDimension
        {
            get { return currentDimension; }
        }

        public char EmptyDrawValue
        {
            get { return emptyDrawValue; }
        }

        public char DefaultDrawValue
        {
            get { return defaultDrawValue; }
        }

        public int DefaultBackgroundColor
        {
            get { return defaultBackgroundColor; }
        }

        public int DefaultForegroundColor
        {
            get { return defaultForegroundColor; }
        }

        public bool Initialize(Dimension2D dimension)
        {
            if (Helper.IsValid(dimension))
            {
                currentDimension = dimension;
                Reset();
                return true;
            }

            return false;
        }

        public void Draw(IEnumerable<DrawUnit> units)
        {
            if (units == null)
                return;

            if (units.Any(u => Helper.Point2DOutsideDimension(currentDimension, u.Coordinate)))
                return;

            foreach (var unit in units)
            {
                if (canvas.ContainsKey(unit.Coordinate))
                {
                    canvas[unit.Coordinate] = unit;
                    continue;
                }

                canvas.Add(unit.Coordinate, unit);
            }
        }

        public IReadOnlyDictionary<Point2D, DrawUnit> Get()
        {
            return canvas;
        }

        public void Reset()
        {
            canvas.Clear();
        }

        public void Draw(DrawUnit unit)
        {
            if (unit == null)
                return;

            if (Helper.Point2DOutsideDimension(currentDimension, unit.Coordinate))
                return;

            if (canvas.ContainsKey(unit.Coordinate))
            {
                canvas[unit.Coordinate] = unit;
                return;
            }

            canvas.Add(unit.Coordinate, unit);
        }
    }
}