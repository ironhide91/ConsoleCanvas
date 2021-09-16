using ConsoleCanvas.Core;
using ConsoleCanvas.Impl.Parameter;
using ConsoleCanvas.Impl.Parser;
using FluentAssertions;
using Xunit;

namespace ConsoleCanvas.Test.Parser
{
    public class CanvasParserTest
    {
        [Theory]
        [InlineData("c 20 04", 20, 04)]
        [InlineData("C 20 04", 20, 04)]
        [InlineData("C 50 30", 50, 30)]
        [InlineData("C 09 03", 09, 03)]
        public void CanParseExpected(string command, int width, int height)
        {
            ICommandParser<CanvasCommandParameter> parser = new CanvasCommandParser();

            var parsed = parser.TryParse(command, out CanvasCommandParameter param);

            parsed.Should().BeTrue();
            param.Should().NotBeNull();
            param.Dimension.Width.Should().Be(width);
            param.Dimension.Height.Should().Be(height);
        }

        [Theory]
        [InlineData(" C  20  04 ", 20, 04)]
        [InlineData("  C    50    30    ", 50, 30)]
        [InlineData("C 09  03 ", 09, 03)]
        public void CanParseExpectedWhitespaces(string command, int width, int height)
        {
            ICommandParser<CanvasCommandParameter> parser = new CanvasCommandParser();

            var parsed = parser.TryParse(command, out CanvasCommandParameter param);

            parsed.Should().BeTrue();
            param.Should().NotBeNull();
            param.Dimension.Width.Should().Be(width);
            param.Dimension.Height.Should().Be(height);
        }

        [Theory]
        [InlineData("C -20  04", -20,  04)]
        [InlineData("C  50 -30",  50, -30)]
        [InlineData("C -09 -03", -09, -03)]
        public void CanParseExpectedNegativeNumbers(string command, int width, int height)
        {
            ICommandParser<CanvasCommandParameter> parser = new CanvasCommandParser();

            var parsed = parser.TryParse(command, out CanvasCommandParameter param);

            parsed.Should().BeTrue();
            param.Should().NotBeNull();
            param.Dimension.Width.Should().Be(width);
            param.Dimension.Height.Should().Be(height);
        }

        [Theory]
        [InlineData(" 2 4")]
        [InlineData("C 3 ")]
        [InlineData("C 3 2l")]
        [InlineData("C 3 2 l")]
        [InlineData("C ")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("C 2 4 2")]
        [InlineData("C 2 4 2 78 ax TE")]
        public void InvalidCommands(string command)
        {
            ICommandParser<CanvasCommandParameter> parser = new CanvasCommandParser();

            var parsed = parser.TryParse(command, out CanvasCommandParameter param);

            parsed.Should().BeFalse();
            param.Should().BeNull();
        }
    }
}
