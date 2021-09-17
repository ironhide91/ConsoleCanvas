using ConsoleCanvas.Core;
using ConsoleCanvas.Impl.Undo;
using System;
using System.Collections.Generic;

namespace ConsoleCanvas.Impl.Executor
{
    public abstract class CommandExecutorBase<T> : ICommandExecutor where T : IDrawRoutineParam
    {
        public CommandExecutorBase(
            ICommandParser<T> parser,
            ICommandValidator<T> validator,
            IDrawCommand<T> drawer,
            IUndo<T> undo)
        {
            if (parser == null)
                throw new ArgumentNullException(nameof(parser));

            if (validator == null)
                throw new ArgumentNullException(nameof(validator));

            if (drawer == null)
                throw new ArgumentNullException(nameof(drawer));

            if (undo == null)
                throw new ArgumentNullException(nameof(undo));

            this.parser = parser;
            this.validator = validator;
            this.drawer = drawer;
            this.undo = undo;
        }

        protected readonly ICommandParser<T> parser;
        protected readonly ICommandValidator<T> validator;
        protected readonly IDrawCommand<T> drawer;
        protected readonly IUndo<T> undo;

        public abstract string CommandKey { get; }

        protected (bool, T) CanExecute(Dimension2D dimension, string command)
        {
            var success = parser.TryParse(command, out T parsedCommand);

            if (!success)
            {
                Logger.Error("Parsing failed");
                return (false, default);
            }

            Logger.Info("Parsing succeeded");

            success = validator.IsValid(parsedCommand, dimension);

            if (!success)
            {
                Logger.Error("Invalid command");
                return (false, default);
            }

            Logger.Info("Valid command");

            return (success, parsedCommand);
        }

        public (bool, IDrawRoutineParam, IEnumerable<DrawUnit>) Execute(IReadOnlyCanvas2D canvas, Dimension2D dimension, string command)
        {
            var result = CanExecute(dimension, command);

            if (result.Item1)
            {
                Logger.Info("Generating DrawUnits ...");
                var units = drawer.GetDrawUnits(canvas, result.Item2);
                Logger.Info("Generated DrawUnits");
                return (result.Item1, result.Item2, units);
            }

            return (false, default, default);
        }

        public void Undo(ICanvas2D canvas, IDrawRoutineParam param, IEnumerable<Point2D> previousCommandPoints)
        {
            undo?.Undo(canvas, (T)param, previousCommandPoints);
        }
    }
}