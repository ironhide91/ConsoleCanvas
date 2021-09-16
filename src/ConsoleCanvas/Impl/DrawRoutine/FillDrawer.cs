using ConsoleCanvas.Core;
using ConsoleCanvas.Impl.Parameter;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ConsoleCanvas.Impl.DrawRoutine
{
    public class FillDrawer : IDrawCommand<FillCommandParameter>
    {
        public IEnumerable<DrawUnit> GetDrawUnits(IReadOnlyCanvas2D canvas, FillCommandParameter parameter)
        {
            return FloodFill(canvas, parameter);
        }

        private static IEnumerable<DrawUnit> FloodFill(IReadOnlyCanvas2D canvas, FillCommandParameter parameter)
        {
            var dict = canvas.Get();
            var seed = parameter.Point;
            var dimension = canvas.CurrentDimension;           
            var queue = new Queue<Point2D>((dimension.Width / dimension.Height) / 2);            

            queue.Enqueue(seed);

            var units = new Dictionary<Point2D, DrawUnit>(100);

            while (queue.TryPeek(out Point2D point))
            {
                if (Helper.Point2DOutsideDimension(dimension, point))
                {
                    _ = queue.Dequeue();
                    continue;
                }

                if (dict.ContainsKey(point) && dict[point].Value != canvas.EmptyDrawValue)
                {
                    _ = queue.Dequeue();
                    continue;
                }

                if (units.ContainsKey(point))
                {
                    _ = queue.Dequeue();
                    continue;
                }

                var unit = new DrawUnit(
                    point,
                    canvas.EmptyDrawValue,
                    backgroundColor: parameter.FillColor,
                    foregroundColor: canvas.DefaultForegroundColor);

                units.Add(queue.Dequeue(), unit);

                Propogate(canvas.CurrentDimension, Top(point), units, queue);
                Propogate(canvas.CurrentDimension, Bottom(point), units, queue);
                Propogate(canvas.CurrentDimension, Left(point), units, queue);
                Propogate(canvas.CurrentDimension, Right(point), units, queue);
            }

            return units.Values;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Point2D Top(Point2D point)
        {
            return new Point2D(point.X, point.Y -1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Point2D Bottom(Point2D point)
        {
            return new Point2D(point.X, point.Y + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Point2D Left(Point2D point)
        {
            return new Point2D(point.X -1, point.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Point2D Right(Point2D point)
        {
            return new Point2D(point.X + 1, point.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Propogate(Dimension2D dimension, Point2D point, IDictionary<Point2D, DrawUnit> processed, Queue<Point2D> queue)
        {
            if (Helper.Point2DOutsideDimension(dimension, point))
                return;

            if (processed.ContainsKey(point))
                return;

            queue.Enqueue(point);
        }

        
    }
}