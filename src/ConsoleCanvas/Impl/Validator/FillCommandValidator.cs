using ConsoleCanvas.Core;
using ConsoleCanvas.Impl.Parameter;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleCanvas.Impl.Validator
{
    public class FillCommandValidator : ICommandValidator<FillCommandParameter>
    {
        private readonly IReadOnlySet<int> ValidConsoleColorValues =
            Enumerable.Range(0, 16).ToHashSet();

        public bool IsValid(FillCommandParameter parameter, Dimension2D dimension)
        {
            if (parameter == null)
                return false;

            if (Helper.Point2DOutsideDimension(dimension, parameter.Point))
                return false;

            return ValidConsoleColorValues.Contains(parameter.FillColor);
        }
    }
}