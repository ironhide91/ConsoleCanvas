namespace ConsoleCanvas.Core
{
    public struct Dimension2D
    {
        public Dimension2D(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public readonly int Width;
        public readonly int Height;

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (obj is null)
                return false;

            if (!(obj is Dimension2D))
                return false;

            var dimension = (Dimension2D)obj;

            return (dimension.Width == Width) && (dimension.Height == Height);
        }

        public override string ToString()
        {
            return $"W {Width}, H {Height}";
        }
    }
}
