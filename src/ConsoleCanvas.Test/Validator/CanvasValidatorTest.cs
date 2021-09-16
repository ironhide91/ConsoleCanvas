using ConsoleCanvas.Core;
using ConsoleCanvas.Impl.Parameter;
using ConsoleCanvas.Impl.Validator;
using FluentAssertions;
using Xunit;

namespace ConsoleCanvas.Test.Validator
{
    public class CanvasValidatorTest
    {
        private static readonly Dimension2D maxDimension = new Dimension2D(99, 99);

        [Fact]
        public void WithinMaxDimension()
        {
            ICommandValidator<CanvasCommandParameter> validator = new CanvasCommandValidator();
            var param = new CanvasCommandParameter(new Dimension2D(20, 4));

            var valid = validator.IsValid(param, maxDimension);

            valid.Should().BeTrue();
        }

        [Fact]
        public void NotWithinMaxDimension()
        {
            ICommandValidator<CanvasCommandParameter> validator = new CanvasCommandValidator();
            var param = new CanvasCommandParameter(new Dimension2D(100, 100));

            var valid = validator.IsValid(param, maxDimension);

            valid.Should().BeFalse();
        }

        [Fact]
        public void ZeroCoordinates()
        {
            ICommandValidator<CanvasCommandParameter> validator = new CanvasCommandValidator();
            var param = new CanvasCommandParameter(new Dimension2D(0, 0));

            var valid = validator.IsValid(param, maxDimension);

            valid.Should().BeFalse();
        }

        [Fact]
        public void NegativeCoordinates()
        {
            ICommandValidator<CanvasCommandParameter> validator = new CanvasCommandValidator();
            var param = new CanvasCommandParameter(new Dimension2D(-20, -4));

            var valid = validator.IsValid(param, maxDimension);

            valid.Should().BeFalse();
        }
    }
}