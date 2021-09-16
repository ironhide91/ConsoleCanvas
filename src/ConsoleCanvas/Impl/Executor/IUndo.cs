﻿namespace ConsoleCanvas.Core
{
    public interface IUndo<T> where T : IDrawRoutineParam
    {
        void Undo(ICanvas2D canvas, T param);
    }
}