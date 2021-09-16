using ConsoleCanvas.Core;
using System.Collections.Generic;

namespace ConsoleCanvas.Impl
{
    public class Point2DComparer : IComparer<Point2D>
    {
        public int Compare(Point2D x, Point2D y)
        {
            if ((x.X == y.X) && (x.Y == y.Y))
                return 0;

            if ((x.X == y.X))
            {
                if (x.Y < y.Y)
                    return -1;

                if (x.Y > y.Y)
                    return 1;
            }

            if ((x.Y == y.Y))
            {
                if (x.X < y.X)
                    return -1;

                if (x.X > y.X)
                    return 1;
            }

            if ((x.X < y.X))
            {
                if (x.Y < y.Y)
                    return -1;

                if (x.Y > y.Y)
                    return 1;
            }

            if ((x.X > y.X))
            {
                if (x.Y < y.Y)
                    return -1;

                if (x.Y > y.Y)
                    return 1;
            }

            if ((x.Y < y.Y))
            {
                if (x.X < y.X)
                    return -1;

                if (x.X > y.X)
                    return 1;
            }

            if ((x.Y > y.Y))
            {
                if (x.X < y.X)
                    return -1;

                if (x.X > y.X)
                    return 1;
            }

            return 0;
        }
    }
}