using ConsoleCanvas.Core;
using ConsoleCanvas.Impl.Parameter;

namespace ConsoleCanvas.Impl.Validator
{
    public class RectCommandValidator : ICommandValidator<RectCommandParameter>
    {
        public bool IsValid(RectCommandParameter parameter, Dimension2D dimension)
        {
            if (parameter == null)
                return false;

            if (parameter.UpperLeftPoint.Equals(parameter.LowerRightPoint))
                return false;

            if (Helper.Point2DOutsideDimension(dimension, parameter.UpperLeftPoint))
                return false;

            if (Helper.Point2DOutsideDimension(dimension, parameter.LowerRightPoint))
                return false;

            if (parameter.UpperLeftPoint.X == parameter.LowerRightPoint.X)
                return false;

            if (parameter.UpperLeftPoint.Y == parameter.LowerRightPoint.Y)
                return false;

            return true;
        }
    }
}