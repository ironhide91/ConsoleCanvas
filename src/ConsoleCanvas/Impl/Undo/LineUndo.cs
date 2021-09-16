using ConsoleCanvas.Core;
using ConsoleCanvas.Impl.Parameter;

namespace ConsoleCanvas.Impl.Undo
{
    public class LineUndo : IUndo<LineCommandParameter>
    {
        public void Undo(ICanvas2D canvas, LineCommandParameter param)
        {
            var set = canvas.Get();

            foreach (var point in param.AssociatedPoints)
            {
                if (set.ContainsKey(point))
                {
                    set[point].Value = canvas.EmptyDrawValue;
                    set[point].BackgroundColor = canvas.DefaultBackgroundColor;
                    set[point].ForegroundColor = canvas.DefaultForegroundColor;
                }
            }
        }
    }
}