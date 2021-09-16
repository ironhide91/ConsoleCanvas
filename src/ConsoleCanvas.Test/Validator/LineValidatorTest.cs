using ConsoleCanvas.Core;
using ConsoleCanvas.Impl.Parameter;
using ConsoleCanvas.Impl.Validator;
using FluentAssertions;
using Xunit;

namespace ConsoleCanvas.Test.Validator
{
    public class LineValidatorTest
    {
        private static readonly Dimension2D canvasDimension = new Dimension2D(20, 4);

        [Fact]
        public void WithinCanvasDimension()
        {
            ICommandValidator<LineCommandParameter> validator = new LineCommandValidator();
            var param = new LineCommandParameter(new Point2D(1, 2), new Point2D(6, 2));

            var valid = validator.IsValid(param, canvasDimension);

            valid.Should().BeTrue();
        }

        [Fact]
        public void Point1NotWithinCanvasDimension()
        {
            ICommandValidator<LineCommandParameter> validator = new LineCommandValidator();
            var param = new LineCommandParameter(new Point2D(21, 2), new Point2D(6, 2));


            var valid = validator.IsValid(param, canvasDimension);

            valid.Should().BeFalse();
        }

        [Fact]
        public void Point2NotWithinCanvasDimension()
        {
            ICommandValidator<LineCommandParameter> validator = new LineCommandValidator();
            var param = new LineCommandParameter(new Point2D(1, 2), new Point2D(6, 27));


            var valid = validator.IsValid(param, canvasDimension);

            valid.Should().BeFalse();
        }

        [Fact]
        public void ZeroCoordinates()
        {
            ICommandValidator<LineCommandParameter> validator = new LineCommandValidator();
            var param = new LineCommandParameter(new Point2D(0, 0), new Point2D(0, 0));


            var valid = validator.IsValid(param, canvasDimension);

            valid.Should().BeFalse();
        }

        [Fact]
        public void NegativeCoordinates()
        {
            ICommandValidator<LineCommandParameter> validator = new LineCommandValidator();
            var param = new LineCommandParameter(new Point2D(-1, -2), new Point2D(-6, -2));

            var valid = validator.IsValid(param, canvasDimension);

            valid.Should().BeFalse();
        }
    }
}