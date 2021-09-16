using ConsoleCanvas.Core;
using ConsoleCanvas.Impl.DrawRoutine;
using ConsoleCanvas.Impl.Parameter;
using ConsoleCanvas.Impl;
using FluentAssertions;
using Xunit;
using System.Linq;

namespace ConsoleCanvas.Test.DrawRoutine
{
    public class RectDrawerTest
    {
        [Fact]
        public void CanDrawSimple()
        {
            IDrawCommand<RectCommandParameter> drawer = new RectDrawer();
            var param = new RectCommandParameter(new Point2D(14, 1), new Point2D(18, 3));
            ICanvas2D canvas = new Canvas2D(' ', 'X', 7, 0, new Point2DComparer());
            canvas.Initialize(new Dimension2D(99, 99));

            var unitsToDraw = drawer.GetDrawUnits(canvas, param);
            canvas.Draw(unitsToDraw);

            var units = canvas.Get();
            units.Should().NotBeNull();
            units.Count.Should().Be(12);
            units.ElementAt(0).Key.Should().Be(14, 1);
            units.ElementAt(1).Key.Should().Be(15, 1);
            units.ElementAt(2).Key.Should().Be(16, 1);
            units.ElementAt(3).Key.Should().Be(17, 1);
            units.ElementAt(4).Key.Should().Be(18, 1);
            units.ElementAt(5).Key.Should().Be(14, 2);            
            units.ElementAt(6).Key.Should().Be(18, 2);
            units.ElementAt(7).Key.Should().Be(14, 3); 
            units.ElementAt(8).Key.Should().Be(15, 3);
            units.ElementAt(9).Key.Should().Be(16, 3);
            units.ElementAt(10).Key.Should().Be(17, 3);
            units.ElementAt(11).Key.Should().Be(18, 3);
        }

        [Fact]
        public void CanDrawRectOnCanvasBoundry()
        {
            var canvasDimension = new Dimension2D(3, 3);
            IDrawCommand<RectCommandParameter> drawer = new RectDrawer();
            var param = new RectCommandParameter(
                new Point2D(1, 1),
                new Point2D(canvasDimension.Width, canvasDimension.Height));
            ICanvas2D canvas = new Canvas2D(' ', 'X', 7, 0, new Point2DComparer());
            canvas.Initialize(canvasDimension);

            var unitsToDraw = drawer.GetDrawUnits(canvas, param);
            canvas.Draw(unitsToDraw);

            var units = canvas.Get();
            units.Should().NotBeNull();
            units.Count.Should().Be(8);
            units.ElementAt(0).Key.Should().Be(1, 1);
            units.ElementAt(1).Key.Should().Be(2, 1);
            units.ElementAt(2).Key.Should().Be(3, 1);
            units.ElementAt(3).Key.Should().Be(1, 2);
            units.ElementAt(4).Key.Should().Be(3, 2);
            units.ElementAt(5).Key.Should().Be(1, 3);
            units.ElementAt(6).Key.Should().Be(2, 3);
            units.ElementAt(7).Key.Should().Be(3, 3);      
        }
    }
}