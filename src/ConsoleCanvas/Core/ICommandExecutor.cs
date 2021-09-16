using System.Collections.Generic;

namespace ConsoleCanvas.Core
{

    public interface ICommandExecutor : ICommandKey
    {
        (bool, IDrawRoutineParam, IEnumerable<DrawUnit>) Execute(IReadOnlyCanvas2D canvas, Dimension2D dimension, string command);

        void Undo(ICanvas2D canvas, IDrawRoutineParam param);
    }
}