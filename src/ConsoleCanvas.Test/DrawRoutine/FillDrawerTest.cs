using ConsoleCanvas.Core;
using ConsoleCanvas.Impl;
using ConsoleCanvas.Impl.DrawRoutine;
using ConsoleCanvas.Impl.Parameter;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace ConsoleCanvas.Test.DrawRoutine
{
    public class FillDrawerTest
    {
        private static readonly Dimension2D canvasDimension = new Dimension2D(99, 99);

        [Fact]
        public void CanFillBlankCanvas()
        {
            var canvasDimension = new Dimension2D(3, 3);
            IDrawCommand<FillCommandParameter> drawer = new FillDrawer();
            var param = new FillCommandParameter(new Point2D(1, 2), 4);
            ICanvas2D canvas = new Canvas2D(' ', 'X', 0, 7, new Point2DComparer());
            canvas.Initialize(canvasDimension);

            var unitsToDraw = drawer.GetDrawUnits(canvas, param);
            canvas.Draw(unitsToDraw);

            var units = canvas.Get();
            units.Should().NotBeNull();
            units.Count.Should().Be(9);
            units.ElementAt(0).Value.Should().Be(1, 1, canvas.EmptyDrawValue, 4);
            units.ElementAt(1).Value.Should().Be(2, 1, canvas.EmptyDrawValue, 4);
            units.ElementAt(2).Value.Should().Be(3, 1, canvas.EmptyDrawValue, 4);
            units.ElementAt(3).Value.Should().Be(1, 2, canvas.EmptyDrawValue, 4);
            units.ElementAt(4).Value.Should().Be(2, 2, canvas.EmptyDrawValue, 4);
            units.ElementAt(5).Value.Should().Be(3, 2, canvas.EmptyDrawValue, 4);
            units.ElementAt(6).Value.Should().Be(1, 3, canvas.EmptyDrawValue, 4);
            units.ElementAt(7).Value.Should().Be(2, 3, canvas.EmptyDrawValue, 4);
            units.ElementAt(8).Value.Should().Be(3, 3, canvas.EmptyDrawValue, 4);
        }

        [Fact]
        public void CanFillOnlyWithinCanvasRectBoundry()
        {
            var canvasDimension = new Dimension2D(4, 4);
            IDrawCommand<FillCommandParameter> drawer = new FillDrawer();
            var fillParam = new FillCommandParameter(new Point2D(2, 2), 4);
            var rectParam = new RectCommandParameter(
                new Point2D(1, 1),
                new Point2D(canvasDimension.Width, canvasDimension.Height));
            ICanvas2D canvas = new Canvas2D(' ', 'X', 0, 7, new Point2DComparer());
            canvas.Initialize(canvasDimension);
            canvas.Draw(new RectDrawer().GetDrawUnits(canvas, rectParam));

            var unitsToDraw = drawer.GetDrawUnits(canvas, fillParam);
            canvas.Draw(unitsToDraw);

            var units = canvas.Get();
            units.Should().NotBeNull();
            units.Count.Should().Be(16);
            // boundry
            units.ElementAt(00).Value.Should().Be(1, 1, canvas.DefaultDrawValue, 0);
            units.ElementAt(01).Value.Should().Be(2, 1, canvas.DefaultDrawValue, 0);
            units.ElementAt(02).Value.Should().Be(3, 1, canvas.DefaultDrawValue, 0);
            units.ElementAt(03).Value.Should().Be(4, 1, canvas.DefaultDrawValue, 0);
            //
            units.ElementAt(04).Value.Should().Be(1, 2, canvas.DefaultDrawValue, 0);
            units.ElementAt(07).Value.Should().Be(4, 2, canvas.DefaultDrawValue, 0);
            units.ElementAt(08).Value.Should().Be(1, 3, canvas.DefaultDrawValue, 0);
            units.ElementAt(11).Value.Should().Be(4, 3, canvas.DefaultDrawValue, 0);            
            //
            units.ElementAt(12).Value.Should().Be(1, 4, canvas.DefaultDrawValue, 0);
            units.ElementAt(13).Value.Should().Be(2, 4, canvas.DefaultDrawValue, 0);
            units.ElementAt(14).Value.Should().Be(3, 4, canvas.DefaultDrawValue, 0);
            units.ElementAt(15).Value.Should().Be(4, 4, canvas.DefaultDrawValue, 0);
            // enclosing
            units.ElementAt(05).Value.Should().Be(2, 2, canvas.EmptyDrawValue, 4);
            units.ElementAt(06).Value.Should().Be(3, 2, canvas.EmptyDrawValue, 4);
            units.ElementAt(09).Value.Should().Be(2, 3, canvas.EmptyDrawValue, 4);
            units.ElementAt(10).Value.Should().Be(3, 3, canvas.EmptyDrawValue, 4);
        }

        [Fact]
        public void CanFillOnlyOutsideRectBoundry()
        {
            var canvasDimension = new Dimension2D(5, 5);
            IDrawCommand<FillCommandParameter> drawer = new FillDrawer();
            var fillParam = new FillCommandParameter(new Point2D(5, 5), 4);
            var rectParam = new RectCommandParameter(
                new Point2D(2, 2),
                new Point2D(4, 4));
            ICanvas2D canvas = new Canvas2D(' ', 'X', 0, 7, new Point2DComparer());
            canvas.Initialize(canvasDimension);
            canvas.Draw(new RectDrawer().GetDrawUnits(canvas, rectParam));

            var unitsToDraw = drawer.GetDrawUnits(canvas, fillParam);
            canvas.Draw(unitsToDraw);

            var units = canvas.Get();
            units.Should().NotBeNull();
            units.Count.Should().Be(24);
            // boundry
            units.ElementAt(00).Value.Should().Be(1, 1, canvas.EmptyDrawValue, 4);
            units.ElementAt(01).Value.Should().Be(2, 1, canvas.EmptyDrawValue, 4);
            units.ElementAt(02).Value.Should().Be(3, 1, canvas.EmptyDrawValue, 4);
            units.ElementAt(03).Value.Should().Be(4, 1, canvas.EmptyDrawValue, 4);
            units.ElementAt(04).Value.Should().Be(5, 1, canvas.EmptyDrawValue, 4);
            //
            units.ElementAt(05).Value.Should().Be(1, 2, canvas.EmptyDrawValue, 4);
            units.ElementAt(09).Value.Should().Be(5, 2, canvas.EmptyDrawValue, 4);
            units.ElementAt(10).Value.Should().Be(1, 3, canvas.EmptyDrawValue, 4);
            units.ElementAt(13).Value.Should().Be(5, 3, canvas.EmptyDrawValue, 4);
            units.ElementAt(14).Value.Should().Be(1, 4, canvas.EmptyDrawValue, 4);
            units.ElementAt(18).Value.Should().Be(5, 4, canvas.EmptyDrawValue, 4);
            //
            units.ElementAt(19).Value.Should().Be(1, 5, canvas.EmptyDrawValue, 4);
            units.ElementAt(20).Value.Should().Be(2, 5, canvas.EmptyDrawValue, 4);
            units.ElementAt(21).Value.Should().Be(3, 5, canvas.EmptyDrawValue, 4);
            units.ElementAt(22).Value.Should().Be(4, 5, canvas.EmptyDrawValue, 4);
            units.ElementAt(23).Value.Should().Be(5, 5, canvas.EmptyDrawValue, 4);
            // rect
            units.ElementAt(06).Value.Should().Be(2, 2, canvas.DefaultDrawValue, 0);
            units.ElementAt(07).Value.Should().Be(3, 2, canvas.DefaultDrawValue, 0);
            units.ElementAt(08).Value.Should().Be(4, 2, canvas.DefaultDrawValue, 0);
            units.ElementAt(11).Value.Should().Be(2, 3, canvas.DefaultDrawValue, 0);
            units.ElementAt(12).Value.Should().Be(4, 3, canvas.DefaultDrawValue, 0);
            units.ElementAt(15).Value.Should().Be(2, 4, canvas.DefaultDrawValue, 0);
            units.ElementAt(16).Value.Should().Be(3, 4, canvas.DefaultDrawValue, 0);
            units.ElementAt(17).Value.Should().Be(4, 4, canvas.DefaultDrawValue, 0);
            // point within rect should not exist
            units.ContainsKey(new Point2D(3, 3)).Should().BeFalse();
        }
    }
}