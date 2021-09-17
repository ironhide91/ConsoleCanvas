namespace ConsoleCanvas.Core
{
    public static class Settings
    {
        private static readonly Dimension2D DefaultMaxDimension =
            new Dimension2D(50, 30);

        public static Dimension2D MaxDimension = DefaultMaxDimension;
    }
}