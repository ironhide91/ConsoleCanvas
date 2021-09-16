using ConsoleCanvas.Core;
using ConsoleCanvas.Impl.Parameter;
using ConsoleCanvas.Impl.Validator;
using FluentAssertions;
using Xunit;

namespace ConsoleCanvas.Test.Validator
{
    public class RectValidatorTest
    {
        private static readonly Dimension2D canvasDimension = new Dimension2D(20, 4);

        [Fact]
        public void WithinCanvasDimension()
        {
            ICommandValidator<RectCommandParameter> validator = new RectCommandValidator();
            var param = new RectCommandParameter(new Point2D(14, 1), new Point2D(18, 3));

            var valid = validator.IsValid(param, canvasDimension);

            valid.Should().BeTrue();
        }

        [Fact]
        public void Point1NotWithinCanvasDimension()
        {
            ICommandValidator<RectCommandParameter> validator = new RectCommandValidator();
            var param = new RectCommandParameter(new Point2D(21, 2), new Point2D(25, 4));

            var valid = validator.IsValid(param, canvasDimension);

            valid.Should().BeFalse();
        }

        [Fact]
        public void Point2NotWithinCanvasDimension()
        {
            ICommandValidator<RectCommandParameter> validator = new RectCommandValidator();
            var param = new RectCommandParameter(new Point2D(1, 2), new Point2D(6, 27));

            var valid = validator.IsValid(param, canvasDimension);

            valid.Should().BeFalse();
        }

        [Fact]
        public void PointsOnXAxis()
        {
            ICommandValidator<RectCommandParameter> validator = new RectCommandValidator();
            var param = new RectCommandParameter(new Point2D(1, 2), new Point2D(1, 3));

            var valid = validator.IsValid(param, canvasDimension);

            valid.Should().BeFalse();
        }

        [Fact]
        public void PointsOnYAxis()
        {
            ICommandValidator<RectCommandParameter> validator = new RectCommandValidator();
            var param = new RectCommandParameter(new Point2D(1, 2), new Point2D(4, 2));

            var valid = validator.IsValid(param, canvasDimension);

            valid.Should().BeFalse();
        }

        [Fact]
        public void ZeroCoordinates()
        {
            ICommandValidator<RectCommandParameter> validator = new RectCommandValidator();
            var param = new RectCommandParameter(new Point2D(0, 0), new Point2D(0, 0));

            var valid = validator.IsValid(param, canvasDimension);

            valid.Should().BeFalse();
        }

        [Fact]
        public void NegativeCoordinates()
        {
            ICommandValidator<RectCommandParameter> validator = new RectCommandValidator();
            var param = new RectCommandParameter(new Point2D(-1, -2), new Point2D(-6, -2));

            var valid = validator.IsValid(param, canvasDimension);

            valid.Should().BeFalse();
        }
    }
}