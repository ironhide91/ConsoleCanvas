namespace ConsoleCanvas.Core
{
    public static class Settings
    {
        private static readonly Dimension2D DefaultMaxDimension =
            new Dimension2D(100, 100);

        public static Dimension2D MaxDimension = DefaultMaxDimension;

        public static void SetMaxDimension(Dimension2D dimension)
        {
            MaxDimension = dimension;
        }
    }
}