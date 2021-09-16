using System.Collections.Generic;

namespace ConsoleCanvas.Core
{
    public interface IUndoPreviousCommand
    {
        void Record(IDrawRoutineParam param);

        bool CanUndo();

        void Undo(ICanvas2D canvas, IReadOnlyDictionary<string, ICommandExecutor> executors);
    }
}