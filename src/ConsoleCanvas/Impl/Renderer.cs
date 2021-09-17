using ConsoleCanvas.Core;

namespace ConsoleCanvas.Impl
{
    public class Renderer : IRender
    {
        public void Render(IReadOnlyCanvas2D canvas, IConsole console)
        {
            Logger.Info("Rendering ...");
            console.NewLine();
            console.Write($"Canvas [{canvas.CurrentDimension.Width} X {canvas.CurrentDimension.Height}]");
            console.NewLine();
            console.NewLine();

            console.Write("|  ");
            int xindex = 1;
            for (int i = 0; i < canvas.CurrentDimension.Width; i++)
            {
                console.Write(string.Format("|{0:00}", xindex++));
            }
            console.Write("|");
            console.NewLine();

            var unitsToDraw = canvas.Get();

            for (int y = 1; y <= canvas.CurrentDimension.Height; y++)
            {
                console.Write(string.Format("|{0:00}|", y));

                for (int x = 1; x < canvas.CurrentDimension.Width; x++)
                {
                    var point = new Point2D(x, y);

                    if (unitsToDraw.ContainsKey(point))
                    {
                        console.Write(" ");
                        console.Write(unitsToDraw[point]);
                        console.Write(" ");
                        continue;
                    }

                    console.Write("   ");
                }

                var lastPointInRow = new Point2D(canvas.CurrentDimension.Width, y);
                if (unitsToDraw.ContainsKey(lastPointInRow))
                {
                    console.Write(" ");
                    console.Write(unitsToDraw[lastPointInRow]);
                }
                else
                {
                    console.Write("  ");
                }

                console.Write("|");
                console.NewLine();
            }

            console.NewLine();
            Logger.Info("Rendered");
        }
    }
}
