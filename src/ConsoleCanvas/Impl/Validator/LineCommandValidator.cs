using ConsoleCanvas.Core;
using ConsoleCanvas.Impl.Parameter;

namespace ConsoleCanvas.Impl.Validator
{
    public class LineCommandValidator : ICommandValidator<LineCommandParameter>
    {
        public bool IsValid(LineCommandParameter parameter, Dimension2D canvasDimension)
        {
            if (parameter == null)
                return false;

            if (Helper.Point2DOutsideDimension(canvasDimension, parameter.Point1))
                return false;

            if (Helper.Point2DOutsideDimension(canvasDimension, parameter.Point2))
                return false;

            return true;
        }
    }
}