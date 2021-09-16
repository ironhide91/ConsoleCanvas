using ConsoleCanvas.Core;
using System.Collections.Generic;

namespace ConsoleCanvas.Impl
{
    public class UndoPreviousCommand : IUndoPreviousCommand
    {
        public UndoPreviousCommand()
        {

        }

        private readonly Stack<IDrawRoutineParam> history = new Stack<IDrawRoutineParam>(10);

        public bool CanUndo()
        {
            return history.TryPeek(out _);
        }

        public void Record(IDrawRoutineParam param)
        {
            history.Push(param);
        }

        public void Undo(ICanvas2D canvas, IReadOnlyDictionary<string, ICommandExecutor> executors)
        {
            Logger.Info("Receieved undo command");

            if (CanUndo())
            {
                var param = history.Pop();

                history.TryPeek(out IDrawRoutineParam prevParam);

                if (executors.ContainsKey(param.CommandKey))
                {
                    Logger.Info("Reverting ...");
                    executors[param.CommandKey].Undo(canvas, param, prevParam?.AssociatedPoints);
                    Logger.Info("Reverted");
                    return;
                }
                
                return;
            }

            Logger.Info("Nothing to revert");
        }
    }
}
