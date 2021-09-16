using ConsoleCanvas.Core;
using ConsoleCanvas.Impl.Parameter;
using System;
using System.Collections.Generic;

namespace ConsoleCanvas.Impl.DrawRoutine
{
    internal static class LineDrawerExtensions
    {
        internal static void DrawHorizontal(Point2D point1, Point2D point2, char value, ICollection<DrawUnit> set)
        {
            for (int x = point1.X; x <= point2.X; x++)
            {
                set.Add(new DrawUnit(new Point2D(x, point1.Y), value));
            }
        }

        internal static void DrawVertical(Point2D point1, Point2D point2, char value, ICollection<DrawUnit> set)
        {
            for (int y = point1.Y; y <= point2.Y; y++)
            {
                set.Add(new DrawUnit(new Point2D(point1.X, y), value));
            }
        }

        internal static void DrawBresenham(this LineCommandParameter parameter, char value, ICollection<DrawUnit> set)
        {
            int x0 = parameter.Point1.X;
            int y0 = parameter.Point1.Y;
            int x1 = parameter.Point2.X;
            int y1 = parameter.Point2.Y;

            int dx = Math.Abs(x1 - x0);
            int dy = Math.Abs(y1 - y0);

            int sx = x0 < x1 ? 1 : -1;
            int sy = y0 < y1 ? 1 : -1;

            int err = (dx > dy ? dx : -dy) / 2;
            int e2;

            while (true)
            {
                var unit = new DrawUnit(new Point2D(x0, y0), value);

                set.Add(unit);

                if (x0 == x1 && y0 == y1)
                    break;

                e2 = err;

                if (e2 > -dx)
                {
                    err -= dy;
                    x0 += sx;
                }

                if (e2 < dy)
                {
                    err += dx;
                    y0 += sy;
                }
            }
        }
    }
}