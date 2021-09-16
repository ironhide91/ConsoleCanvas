namespace ConsoleCanvas.Core
{
    public static class Helper
    {
        public static bool IsValid(this Dimension2D dimension)
        {
            return (dimension.Width > 1) && (dimension.Height > 1);
        }

        public static bool IsNotSubset(this Dimension2D dimension, Dimension2D outerDimension)
        {
            if (dimension.Width < 1)
                return true;

            if (dimension.Height < 1)
                return true;

            return (dimension.Width > outerDimension.Width) || (dimension.Height > outerDimension.Height);
        }

        public static bool Point2DOutsideDimension(Dimension2D dimension, Point2D point)
        {
            if (point.X < 1)
                return true;

            if (point.Y < 1)
                return true;

            return (point.X > dimension.Width) || (point.Y > dimension.Height);
        }
    }
}