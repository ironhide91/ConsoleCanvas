using ConsoleCanvas.Core;
using ConsoleCanvas.Impl;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace ConsoleCanvas.Test
{
    public class CanvasTest
    {
        [Fact]
        public void ZeroDimensionOnNoninitialize()
        {
            ICanvas2D canvas = new Canvas2D(' ', 'X', 7, 0, new Point2DComparer());

            canvas.CurrentDimension.Width.Should().Be(0);
            canvas.CurrentDimension.Height.Should().Be(0);
        }

        [Theory]
        [InlineData(-1, -1)]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        public void FailOnInvalidDimension(int width, int height)
        {
            ICanvas2D canvas = new Canvas2D(' ', 'X', 7, 0, new Point2DComparer());
            var dimension = new Dimension2D(width, height);

            var success = canvas.Initialize(dimension);

            success.Should().BeFalse();
        }

        [Fact]
        public void CanInitialize()
        {
            ICanvas2D canvas = new Canvas2D(' ', 'X', 7, 0, new Point2DComparer());
            var dimension = new Dimension2D(20, 4);

            var success = canvas.Initialize(dimension);

            success.Should().BeTrue();
        }

        [Fact]
        public void CanGetCurrentDimensionOnInitialize()
        {
            ICanvas2D canvas = new Canvas2D(' ', 'X', 7, 0, new Point2DComparer());
            var dimension = new Dimension2D(20, 4);

            canvas.Initialize(dimension);

            canvas.CurrentDimension.Should().Be(dimension);
        }

        [Fact]
        public void CanDrawUnit()
        {
            ICanvas2D canvas = new Canvas2D(' ', 'X', 7, 0, new Point2DComparer());
            var dimension = new Dimension2D(20, 4);
            canvas.Initialize(dimension);
            var unit = new DrawUnit(new Point2D(3,3), 'X');

            canvas.Draw(unit);

            canvas.Get().Count.Should().Be(1);
            canvas.Get().ElementAt(0).Value.Coordinate.X.Should().Be(3);
            canvas.Get().ElementAt(0).Value.Coordinate.Y.Should().Be(3);
            canvas.Get().ElementAt(0).Value.Value.Should().Be('X');
            canvas.Get().ElementAt(0).Value.ForegroundColor.Should().Be(7);
            canvas.Get().ElementAt(0).Value.BackgroundColor.Should().Be(0);
        }

        [Fact]
        public void CanDrawMultipleUnits()
        {
            ICanvas2D canvas = new Canvas2D(' ', 'X', 7, 0, new Point2DComparer());
            var dimension = new Dimension2D(20, 4);
            canvas.Initialize(dimension);
            var unit1 = new DrawUnit(new Point2D(18, 3), 'X');
            var unit2 = new DrawUnit(new Point2D(1, 1), 'Y');
            var unit3 = new DrawUnit(new Point2D(3, 2), 'Z');
            var unit4 = new DrawUnit(new Point2D(20, 4), 'Q');

            canvas.Draw(unit1);
            canvas.Draw(unit2);
            canvas.Draw(unit3);
            canvas.Draw(unit4);

            canvas.Get().Count.Should().Be(4);
            //
            canvas.Get().ElementAt(0).Value.Coordinate.X.Should().Be(1);
            canvas.Get().ElementAt(0).Value.Coordinate.Y.Should().Be(1);
            canvas.Get().ElementAt(0).Value.Value.Should().Be('Y');
            canvas.Get().ElementAt(0).Value.ForegroundColor.Should().Be(7);
            canvas.Get().ElementAt(0).Value.BackgroundColor.Should().Be(0);
            //
            canvas.Get().ElementAt(1).Value.Coordinate.X.Should().Be(3);
            canvas.Get().ElementAt(1).Value.Coordinate.Y.Should().Be(2);
            canvas.Get().ElementAt(1).Value.Value.Should().Be('Z');
            canvas.Get().ElementAt(1).Value.ForegroundColor.Should().Be(7);
            canvas.Get().ElementAt(1).Value.BackgroundColor.Should().Be(0);
            //
            canvas.Get().ElementAt(2).Value.Coordinate.X.Should().Be(18);
            canvas.Get().ElementAt(2).Value.Coordinate.Y.Should().Be(3);
            canvas.Get().ElementAt(2).Value.Value.Should().Be('X');
            canvas.Get().ElementAt(2).Value.ForegroundColor.Should().Be(7);
            canvas.Get().ElementAt(2).Value.BackgroundColor.Should().Be(0);
            //
            canvas.Get().ElementAt(3).Value.Coordinate.X.Should().Be(20);
            canvas.Get().ElementAt(3).Value.Coordinate.Y.Should().Be(4);
            canvas.Get().ElementAt(3).Value.Value.Should().Be('Q');
            canvas.Get().ElementAt(3).Value.ForegroundColor.Should().Be(7);
            canvas.Get().ElementAt(3).Value.BackgroundColor.Should().Be(0);
        }

        [Fact]
        public void CanDrawCollectionOfUnits()
        {
            ICanvas2D canvas = new Canvas2D(' ', 'X', 7, 0, new Point2DComparer());
            var dimension = new Dimension2D(20, 4);
            canvas.Initialize(dimension);
            var unit1 = new DrawUnit(new Point2D(18, 3), 'X');
            var unit2 = new DrawUnit(new Point2D(1, 1), 'Y');
            var unit3 = new DrawUnit(new Point2D(3, 2), 'Z');
            var unit4 = new DrawUnit(new Point2D(20, 4), 'Q');

            canvas.Draw(new DrawUnit[] { unit1, unit2, unit3, unit4 });

            canvas.Get().Count.Should().Be(4);
            //
            canvas.Get().ElementAt(0).Value.Coordinate.X.Should().Be(1);
            canvas.Get().ElementAt(0).Value.Coordinate.Y.Should().Be(1);
            canvas.Get().ElementAt(0).Value.Value.Should().Be('Y');
            canvas.Get().ElementAt(0).Value.ForegroundColor.Should().Be(7);
            canvas.Get().ElementAt(0).Value.BackgroundColor.Should().Be(0);
            //
            canvas.Get().ElementAt(1).Value.Coordinate.X.Should().Be(3);
            canvas.Get().ElementAt(1).Value.Coordinate.Y.Should().Be(2);
            canvas.Get().ElementAt(1).Value.Value.Should().Be('Z');
            canvas.Get().ElementAt(1).Value.ForegroundColor.Should().Be(7);
            canvas.Get().ElementAt(1).Value.BackgroundColor.Should().Be(0);
            //
            canvas.Get().ElementAt(2).Value.Coordinate.X.Should().Be(18);
            canvas.Get().ElementAt(2).Value.Coordinate.Y.Should().Be(3);
            canvas.Get().ElementAt(2).Value.Value.Should().Be('X');
            canvas.Get().ElementAt(2).Value.ForegroundColor.Should().Be(7);
            canvas.Get().ElementAt(2).Value.BackgroundColor.Should().Be(0);
            //
            canvas.Get().ElementAt(3).Value.Coordinate.X.Should().Be(20);
            canvas.Get().ElementAt(3).Value.Coordinate.Y.Should().Be(4);
            canvas.Get().ElementAt(3).Value.Value.Should().Be('Q');
            canvas.Get().ElementAt(3).Value.ForegroundColor.Should().Be(7);
            canvas.Get().ElementAt(3).Value.BackgroundColor.Should().Be(0);
        }

        [Fact]
        public void CanReset()
        {
            ICanvas2D canvas = new Canvas2D(' ', 'X', 7, 0, new Point2DComparer());
            var beforeReset = new Dimension2D(20, 4);
            //var afterReset = new Dimension2D(5, 5);
            canvas.Initialize(beforeReset);
            var unit1 = new DrawUnit(new Point2D(18, 3), 'X');
            var unit2 = new DrawUnit(new Point2D(1, 1), 'Y');
            var unit3 = new DrawUnit(new Point2D(3, 2), 'Z');
            var unit4 = new DrawUnit(new Point2D(20, 4), 'Q');
            canvas.Draw(new DrawUnit[] { unit1, unit2, unit3, unit4 });

            canvas.Reset();

            canvas.Get().Count.Should().Be(0);
        }

        [Fact]
        public void CanReinitializeAndDrawAfterReset()
        {
            ICanvas2D canvas = new Canvas2D(' ', 'X', 7, 0, new Point2DComparer());
            var beforeReset = new Dimension2D(5, 5);
            var afterReset = new Dimension2D(20, 20);
            canvas.Initialize(beforeReset);
            var unit = new DrawUnit(new Point2D(6, 6), 'X');
            canvas.Draw(unit);

            canvas.Get().Count.Should().Be(0);
            canvas.Reset();
            var success = canvas.Initialize(afterReset);
            success.Should().BeTrue();
            canvas.Draw(unit);
            canvas.Get().Count.Should().Be(1);
            canvas.Get().ElementAt(0).Value.Coordinate.X.Should().Be(6);
            canvas.Get().ElementAt(0).Value.Coordinate.Y.Should().Be(6);
            canvas.Get().ElementAt(0).Value.Value.Should().Be('X');
            canvas.Get().ElementAt(0).Value.ForegroundColor.Should().Be(7);
            canvas.Get().ElementAt(0).Value.BackgroundColor.Should().Be(0);
        }
    }
}