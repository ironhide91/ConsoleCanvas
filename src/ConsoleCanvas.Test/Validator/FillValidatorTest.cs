using ConsoleCanvas.Core;
using ConsoleCanvas.Impl.Parameter;
using ConsoleCanvas.Impl.Validator;
using FluentAssertions;
using Xunit;

namespace ConsoleCanvas.Test.Validator
{
    public class FillValidatorTest
    {
        private static readonly Dimension2D canvasDimension = new Dimension2D(99, 99);

        [Fact]
        public void WithinMaxDimension()
        {
            ICommandValidator<FillCommandParameter> validator = new FillCommandValidator();
            var param = new FillCommandParameter(new Point2D(20, 4), 0);

            var valid = validator.IsValid(param, canvasDimension);

            valid.Should().BeTrue();
        }

        [Fact]
        public void NotWithinMaxDimension()
        {
            ICommandValidator<FillCommandParameter> validator = new FillCommandValidator();
            var param = new FillCommandParameter(new Point2D(100, 100), 0);

            var valid = validator.IsValid(param, canvasDimension);

            valid.Should().BeFalse();
        }

        [Fact]
        public void ZeroCoordinates()
        {
            ICommandValidator<FillCommandParameter> validator = new FillCommandValidator();
            var param = new FillCommandParameter(new Point2D(0, 0), 0);

            var valid = validator.IsValid(param, canvasDimension);

            valid.Should().BeFalse();
        }

        [Fact]
        public void NegativeCoordinates()
        {
            ICommandValidator<FillCommandParameter> validator = new FillCommandValidator();
            var param = new FillCommandParameter(new Point2D(-20, -4), 0);

            var valid = validator.IsValid(param, canvasDimension);

            valid.Should().BeFalse();
        }

        [Theory]
        [InlineData(16)]
        [InlineData(-16)]
        public void InvalidFillColor(int fillColor)
        {
            ICommandValidator<FillCommandParameter> validator = new FillCommandValidator();
            var param = new FillCommandParameter(new Point2D(10, 2), fillColor);

            var valid = validator.IsValid(param, canvasDimension);

            valid.Should().BeFalse();
        }
    }
}