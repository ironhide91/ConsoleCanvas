using ConsoleCanvas.Core;
using ConsoleCanvas.Impl.Parameter;

namespace ConsoleCanvas.Impl.Validator
{
    public class CanvasCommandValidator : ICommandValidator<CanvasCommandParameter>
    {
        public bool IsValid(CanvasCommandParameter parameter, Dimension2D maxDimension)
        {
            if (parameter == null)
                return false;

            if (parameter.Dimension.IsNotSubset(maxDimension))
                return false;

            return true;
        }
    }
}