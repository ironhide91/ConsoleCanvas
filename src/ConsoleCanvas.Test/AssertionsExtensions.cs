using ConsoleCanvas.Core;
using FluentAssertions;
using FluentAssertions.Primitives;
using FluentAssertions.Execution;

namespace ConsoleCanvas.Test
{
    public static class AssertionsExtensions
    {
        public static Point2DAssertions Should(this Point2D point)
        {
            return new Point2DAssertions(point);
        }

        public class Point2DAssertions : ReferenceTypeAssertions<Point2D, Point2DAssertions>
        {
            public Point2DAssertions(Point2D instance) : base(instance)
            {

            }

            protected override string Identifier => "point";

            public AndConstraint<Point2DAssertions> Be(int expectedX, int expectedY, string because = "", params object[] becauseArgs)
            {
                Execute.Assertion
                    .ForCondition(Subject.X == expectedX && Subject.Y == expectedY)
                    .BecauseOf(because, becauseArgs)
                    .FailWith("Expected to be {0},{1} {reason}, but found {2}.", expectedX, expectedY, Subject.ToString());

                return new AndConstraint<Point2DAssertions>(this);
            }
        }

        public static DrawUnitAssertions Should(this DrawUnit point)
        {
            return new DrawUnitAssertions(point);
        }

        public class DrawUnitAssertions : ReferenceTypeAssertions<DrawUnit, DrawUnitAssertions>
        {
            public DrawUnitAssertions(DrawUnit instance) : base(instance)
            {

            }

            protected override string Identifier => "drawUnit";

            public AndConstraint<DrawUnitAssertions> Be(
                int expectedX,
                int expectedY,
                char value,
                int color,
                string because = "",
                params object[] becauseArgs)
            {
                Execute.Assertion
                    .ForCondition(Subject.Coordinate.X == expectedX && Subject.Coordinate.Y == expectedY && Subject.Value == value && Subject.BackgroundColor == color)
                    .BecauseOf(because, becauseArgs)
                    .FailWith("Expected to be {0},{1},{2} {reason}, but found {3}.", expectedX, expectedY, Subject.ToString());

                return new AndConstraint<DrawUnitAssertions>(this);
            }
        }
    }
}