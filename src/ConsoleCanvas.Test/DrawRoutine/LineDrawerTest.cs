using ConsoleCanvas.Core;
using ConsoleCanvas.Impl.DrawRoutine;
using ConsoleCanvas.Impl.Parameter;
using ConsoleCanvas.Impl;
using FluentAssertions;
using Xunit;
using System.Linq;

namespace ConsoleCanvas.Test.DrawRoutine
{
    public class LineDrawerTest
    {
        private static readonly Dimension2D canvasDimension = new Dimension2D(99, 99);

        [Fact]
        public void CanDraw1()
        {
            IDrawCommand<LineCommandParameter> drawer = new LineDrawer();
            var param = new LineCommandParameter(new Point2D(1, 2), new Point2D(6, 2));
            ICanvas2D canvas = new Canvas2D(' ', 'X', 7, 0, new Point2DComparer());
            canvas.Initialize(canvasDimension);

            var unitsToDraw = drawer.GetDrawUnits(canvas, param);
            canvas.Draw(unitsToDraw);

            var units = canvas.Get();
            units.Should().NotBeNull();
            units.Count.Should().Be(6);
            units.ElementAt(0).Key.Should().Be(1, 2);
            units.ElementAt(1).Key.Should().Be(2, 2);
            units.ElementAt(2).Key.Should().Be(3, 2);
            units.ElementAt(3).Key.Should().Be(4, 2);
            units.ElementAt(4).Key.Should().Be(5, 2);
            units.ElementAt(5).Key.Should().Be(6, 2);
        }

        [Fact]
        public void CanDraw2()
        {
            IDrawCommand<LineCommandParameter> drawer = new LineDrawer();
            var param = new LineCommandParameter(new Point2D(6, 3), new Point2D(6, 4));
            ICanvas2D canvas = new Canvas2D(' ', 'X', 7, 0, new Point2DComparer());
            canvas.Initialize(canvasDimension);

            var unitsToDraw = drawer.GetDrawUnits(canvas, param);
            canvas.Draw(unitsToDraw);

            var units = canvas.Get();
            units.Should().NotBeNull();
            units.Count.Should().Be(2);
            units.ElementAt(0).Key.Should().Be(6, 3);
            units.ElementAt(1).Key.Should().Be(6, 4);
        }
    }
}