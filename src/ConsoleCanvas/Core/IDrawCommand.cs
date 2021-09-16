using System.Collections.Generic;

namespace ConsoleCanvas.Core
{
    public interface IDrawCommand<T> where T : IDrawRoutineParam
    {
        IEnumerable<DrawUnit> GetDrawUnits(IReadOnlyCanvas2D canvas, T parameter);
    }
}