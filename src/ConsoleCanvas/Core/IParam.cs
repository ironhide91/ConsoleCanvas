using System.Collections.Generic;

namespace ConsoleCanvas.Core
{
    public interface IParam : ICommandKey
    {

    }

    public interface IDrawRoutineParam : IParam
    {
        IEnumerable<Point2D> AssociatedPoints { get; set; }
        int BackgroundColorBeforeFill { get; set; }
    }
}