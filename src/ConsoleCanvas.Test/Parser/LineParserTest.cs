using ConsoleCanvas.Core;
using ConsoleCanvas.Impl.Parameter;
using ConsoleCanvas.Impl.Parser;
using FluentAssertions;
using Xunit;

namespace ConsoleCanvas.Test.Parser
{
    public class LineParserTest
    {
        [Theory]
        [InlineData("l 1 2 6 2", 1, 2, 6, 2)]
        [InlineData("L 1 2 6 2", 1, 2, 6, 2)]        
        [InlineData("L 01 02 06 02", 1, 2, 6, 2)]
        [InlineData("L 06 03 06 04", 6, 3, 6, 4)]
        [InlineData("L 10 22 07 45", 10, 22, 7, 45)]
        public void CanParseExpected(string command, int x1, int y1, int x2, int y2)
        {
            ICommandParser<LineCommandParameter> parser = new LineCommandParser();

            var parsed = parser.TryParse(command, out LineCommandParameter param);

            parsed.Should().BeTrue();
            param.Should().NotBeNull();
            param.Point1.X.Should().Be(x1);
            param.Point1.Y.Should().Be(y1);
            param.Point2.X.Should().Be(x2);
            param.Point2.Y.Should().Be(y2);
        }

        [Theory]
        [InlineData("L 1 2 6 2 ", 1, 2, 6, 2)]
        [InlineData(" L 01 02 06 02", 1, 2, 6, 2)]
        [InlineData(" L 06 03 06 04 ", 6, 3, 6, 4)]
        [InlineData(" L  10 22 07 45", 10, 22, 7, 45)]
        [InlineData(" L 10  22 07 45", 10, 22, 7, 45)]
        [InlineData(" L 10 22  07 45", 10, 22, 7, 45)]
        [InlineData(" L 10 22 07  45", 10, 22, 7, 45)]
        [InlineData("   L   10  22      07  45   ", 10, 22, 7, 45)]
        public void CanParseExpectedWhitespaces(string command, int x1, int y1, int x2, int y2)
        {
            ICommandParser<LineCommandParameter> parser = new LineCommandParser();

            var parsed = parser.TryParse(command, out LineCommandParameter param);

            parsed.Should().BeTrue();
            param.Should().NotBeNull();
            param.Point1.X.Should().Be(x1);
            param.Point1.Y.Should().Be(y1);
            param.Point2.X.Should().Be(x2);
            param.Point2.Y.Should().Be(y2);
        }

        [Theory]
        [InlineData("L -1 2 -6 2", -1, 2, -6, 2)]
        [InlineData("L -01 -02 06 -02", -1, -2, 6, -2)]
        [InlineData("L -06 03 06 -04", -6, 3, 6, -4)]
        [InlineData("L -10 -22 -07 -45", -10, -22, -7, -45)]
        public void CanParseExpectedNegativeNumbers(string command, int x1, int y1, int x2, int y2)
        {
            ICommandParser<LineCommandParameter> parser = new LineCommandParser();

            var parsed = parser.TryParse(command, out LineCommandParameter param);

            parsed.Should().BeTrue();
            param.Should().NotBeNull();
            param.Point1.X.Should().Be(x1);
            param.Point1.Y.Should().Be(y1);
            param.Point2.X.Should().Be(x2);
            param.Point2.Y.Should().Be(y2);
        }

        [Theory]
        [InlineData(" 1 2 6 2")]
        [InlineData("L 1 2 6 ")]
        [InlineData("L 1 2 6 2p")]
        [InlineData("L 1 2 6 2 p")]
        [InlineData("L 1 2 ")]
        [InlineData("L 1 ")]
        [InlineData("L ")]
        [InlineData("L")]
        [InlineData("L q")]
        [InlineData(" ")]
        public void InvalidCommands(string command)
        {
            ICommandParser<LineCommandParameter> parser = new LineCommandParser();

            var parsed = parser.TryParse(command, out LineCommandParameter param);

            parsed.Should().BeFalse();
            param.Should().BeNull();
        }
    }
}