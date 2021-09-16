using ConsoleCanvas.Core;
using ConsoleCanvas.Impl.Parameter;
using ConsoleCanvas.Impl.Parser;
using FluentAssertions;
using Xunit;
using System;

namespace ConsoleCanvas.Test.Parser
{
    public class FillParserTest
    {
        [Theory]
        [InlineData("b 20 04 4", 20, 04, ConsoleColor.DarkRed)]
        [InlineData("B 20 04 5", 20, 04, ConsoleColor.DarkMagenta)]
        [InlineData("B 50 30 6", 50, 30, ConsoleColor.DarkYellow)]
        [InlineData("B 09 03 7", 09, 03, ConsoleColor.Gray)]
        public void CanParseExpected(string command, int x, int y, ConsoleColor color)
        {
            ICommandParser<FillCommandParameter> parser = new FillCommandParser();

            var parsed = parser.TryParse(command, out FillCommandParameter param);

            parsed.Should().BeTrue();
            param.Should().NotBeNull();
            param.Point.X.Should().Be(x);
            param.Point.Y.Should().Be(y);
            param.FillColor.Should().Be((int)color);
        }

        [Theory]
        [InlineData(" b 20 04 4", 20, 04, ConsoleColor.DarkRed)]
        [InlineData("B 20 04 5 ", 20, 04, ConsoleColor.DarkMagenta)]
        [InlineData(" B 50 30 6 ", 50, 30, ConsoleColor.DarkYellow)]
        [InlineData(" B  09 03 7", 09, 03, ConsoleColor.Gray)]
        [InlineData(" B 09  03 7", 09, 03, ConsoleColor.Gray)]
        [InlineData(" B 09 03  7", 09, 03, ConsoleColor.Gray)]
        [InlineData("  B 09   03  7   ", 09, 03, ConsoleColor.Gray)]
        public void CanParseExpectedWhitespaces(string command, int x, int y, ConsoleColor color)
        {
            ICommandParser<FillCommandParameter> parser = new FillCommandParser();

            var parsed = parser.TryParse(command, out FillCommandParameter param);

            parsed.Should().BeTrue();
            param.Should().NotBeNull();
            param.Point.X.Should().Be(x);
            param.Point.Y.Should().Be(y);
            param.FillColor.Should().Be((int)color);
        }

        [Theory]
        [InlineData("b -20 -04 4", -20, -04, ConsoleColor.DarkRed)]
        [InlineData("B 20 -04 5", 20, -04, ConsoleColor.DarkMagenta)]
        [InlineData("B -50 30 6", -50, 30, ConsoleColor.DarkYellow)]
        [InlineData("B -09 -03 7", -09, -03, ConsoleColor.Gray)]
        public void CanParseExpectedNegativeNumbers(string command, int x, int y, ConsoleColor color)
        {
            ICommandParser<FillCommandParameter> parser = new FillCommandParser();

            var parsed = parser.TryParse(command, out FillCommandParameter param);

            parsed.Should().BeTrue();
            param.Should().NotBeNull();
            param.Point.X.Should().Be(x);
            param.Point.Y.Should().Be(y);
            param.FillColor.Should().Be((int)color);
        }

        [Theory]
        [InlineData("B 20 04 ")]
        [InlineData("B 20 ")]
        [InlineData("B ")]
        [InlineData("B")]
        [InlineData(" ")]
        [InlineData("")]
        public void InvalidCommands(string command)
        {
            ICommandParser<FillCommandParameter> parser = new FillCommandParser();

            var parsed = parser.TryParse(command, out FillCommandParameter param);

            parsed.Should().BeFalse();
            param.Should().BeNull();
        }
    }
}
