using ConsoleCanvas.Core;
using ConsoleCanvas.Impl.Parameter;
using ConsoleCanvas.Impl.Parser;
using FluentAssertions;
using Xunit;

namespace ConsoleCanvas.Test.Parser
{
    public class RectParserTest
    {
        [Theory]
        [InlineData("r 1 2 6 2", 1, 2, 6, 2)]
        [InlineData("R 1 2 6 2", 1, 2, 6, 2)]        
        [InlineData("R 01 02 06 02", 1, 2, 6, 2)]
        [InlineData("R 06 03 06 04", 6, 3, 6, 4)]
        [InlineData("R 10 22 07 45", 10, 22, 7, 45)]
        public void CanParseExpected(string command, int x1, int y1, int x2, int y2)
        {
            ICommandParser<RectCommandParameter> parser = new RectCommandParser();

            var parsed = parser.TryParse(command, out RectCommandParameter param);

            parsed.Should().BeTrue();
            param.Should().NotBeNull();
            param.UpperLeftPoint.X.Should().Be(x1);
            param.UpperLeftPoint.Y.Should().Be(y1);
            param.LowerRightPoint.X.Should().Be(x2);
            param.LowerRightPoint.Y.Should().Be(y2);
        }

        [Theory]
        [InlineData("R 1 2 6 2 ", 1, 2, 6, 2)]
        [InlineData(" R 01 02 06 02", 1, 2, 6, 2)]
        [InlineData(" R 06 03 06 04 ", 6, 3, 6, 4)]
        [InlineData(" R  10 22 07 45", 10, 22, 7, 45)]
        [InlineData(" R 10  22 07 45", 10, 22, 7, 45)]
        [InlineData(" R 10 22  07 45", 10, 22, 7, 45)]
        [InlineData(" R 10 22 07  45", 10, 22, 7, 45)]
        [InlineData("   R   10  22      07  45   ", 10, 22, 7, 45)]
        public void CanParseExpectedWhitespaces(string command, int x1, int y1, int x2, int y2)
        {
            ICommandParser<RectCommandParameter> parser = new RectCommandParser();

            var parsed = parser.TryParse(command, out RectCommandParameter param);

            parsed.Should().BeTrue();
            param.Should().NotBeNull();
            param.UpperLeftPoint.X.Should().Be(x1);
            param.UpperLeftPoint.Y.Should().Be(y1);
            param.LowerRightPoint.X.Should().Be(x2);
            param.LowerRightPoint.Y.Should().Be(y2);
        }

        [Theory]
        [InlineData("R 1 2 6 2", 1, 2, 6, 2)]
        [InlineData("R 01 02 06 02", 1, 2, 6, 2)]
        [InlineData("R 06 03 06 04", 6, 3, 6, 4)]
        [InlineData("R 10 22 07 45", 10, 22, 7, 45)]
        public void CanParseExpectedNegativeNumbers(string command, int x1, int y1, int x2, int y2)
        {
            ICommandParser<RectCommandParameter> parser = new RectCommandParser();

            var parsed = parser.TryParse(command, out RectCommandParameter param);

            parsed.Should().BeTrue();
            param.Should().NotBeNull();
            param.UpperLeftPoint.X.Should().Be(x1);
            param.UpperLeftPoint.Y.Should().Be(y1);
            param.LowerRightPoint.X.Should().Be(x2);
            param.LowerRightPoint.Y.Should().Be(y2);
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
            ICommandParser<RectCommandParameter> parser = new RectCommandParser();

            var parsed = parser.TryParse(command, out RectCommandParameter param);

            parsed.Should().BeFalse();
            param.Should().BeNull();
        }
    }
}