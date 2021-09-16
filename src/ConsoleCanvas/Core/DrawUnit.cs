using System;

namespace ConsoleCanvas.Core
{
    public class DrawUnit
    {
        public DrawUnit(
            Point2D coordinate,
            char value,
            int foregroundColor = 7,
            int backgroundColor = 0)
        {
            Coordinate = coordinate;
            Value = value;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
        }

        public readonly Point2D Coordinate;

        public char Value { get; set; }
        public int ForegroundColor { get; set; }
        public int BackgroundColor { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (obj is null)
                return false;

            if (!(obj is DrawUnit))
                return false;

            var unit = (DrawUnit)obj;

            var isEqual = (unit.Coordinate.X == Coordinate.X)
                && (unit.Coordinate.Y == Coordinate.Y)
                && (unit.Value == Value)
                && (unit.ForegroundColor == ForegroundColor)
                && (unit.BackgroundColor == BackgroundColor);

            return isEqual;
        }

        public override string ToString()
        {
            return $"{Coordinate} | V {Value} | BG {BackgroundColor} | FG {ForegroundColor}";
        }
    }
}