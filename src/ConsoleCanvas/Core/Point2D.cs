namespace ConsoleCanvas.Core
{
    public struct Point2D
    {
        public Point2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public readonly int X;
        public readonly int Y;

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (obj is null)
                return false;

            if (!(obj is Point2D))
                return false;

            var point = (Point2D)obj;

            return (point.X == X) && (point.Y == Y);
        }

        public override string ToString()
        {
            return $"{X},{Y}";
        }
    }
}