using ConsoleCanvas.Core;
using ConsoleCanvas.Impl.Parameter;
using ConsoleCanvas.Impl.Validator;
using ConsoleCanvas.Impl.DrawRoutine;
using ConsoleCanvas.Impl.Undo;
using ConsoleCanvas.Impl.Parser;
using ConsoleCanvas.Impl.Executor;

namespace ConsoleCanvas.Impl
{
    public static class Base
    {
        internal static readonly ICommandIdentifier commandIdentifier =
            new CommandIdentifier();

        internal static readonly IDrawCanvasCommand drawCanvasCommand =
            new CanvasCommandExecutor(Parsers.canvasParser, Validators.canvasValidator);

        public static ICommandIdentifier CommandIdentifier(this ICore core)
        {
            return commandIdentifier;
        }

        public static IDrawCanvasCommand DrawCanvasCommand(this ICore core)
        {
            return drawCanvasCommand;
        }
    }

    public static class Parsers
    {
        internal static readonly ICommandParser<CanvasCommandParameter> canvasParser =
            new CanvasCommandParser();

        internal static readonly ICommandParser<LineCommandParameter> lineParser =
            new LineCommandParser();

        internal static readonly ICommandParser<RectCommandParameter> rectParser =
            new RectCommandParser();

        internal static readonly ICommandParser<FillCommandParameter> fillParser =
            new FillCommandParser();

        public static ICommandParser<CanvasCommandParameter> CanvasParser(this ICore core)
        {
            return canvasParser;
        }

        public static ICommandParser<LineCommandParameter> LineParser(this ICore core)
        {
            return lineParser;
        }

        public static ICommandParser<RectCommandParameter> RectParser(this ICore core)
        {
            return rectParser;
        }

        public static ICommandParser<FillCommandParameter> FillParser(this ICore core)
        {
            return fillParser;
        }
    }

    public static class Validators
    {
        internal static readonly ICommandValidator<CanvasCommandParameter> canvasValidator =
            new CanvasCommandValidator();

        internal static readonly ICommandValidator<LineCommandParameter> lineValidator =
            new LineCommandValidator();

        internal static readonly ICommandValidator<RectCommandParameter> rectValidator =
            new RectCommandValidator();

        internal static readonly ICommandValidator<FillCommandParameter> fillValidator =
            new FillCommandValidator();

        public static ICommandValidator<CanvasCommandParameter> CanvasValidator(this ICore core)
        {
            return canvasValidator;
        }

        public static ICommandValidator<LineCommandParameter> LineValidator(this ICore core)
        {
            return lineValidator;
        }

        public static ICommandValidator<RectCommandParameter> RectValidator(this ICore core)
        {
            return rectValidator;
        }

        public static ICommandValidator<FillCommandParameter> FillValidator(this ICore core)
        {
            return fillValidator;
        }
    }

    public static class DrawRoutines
    {
        internal static readonly IDrawCommand<LineCommandParameter> lineDrawRoutine =
            new LineDrawer();

        internal static readonly IDrawCommand<RectCommandParameter> rectDrawRoutine =
            new RectDrawer();

        internal static readonly IDrawCommand<FillCommandParameter> fillDrawRoutine =
            new FillDrawer();

        public static IDrawCommand<LineCommandParameter> LineDrawRoutine(this ICore core)
        {
            return lineDrawRoutine;
        }

        public static IDrawCommand<RectCommandParameter> RectDrawRoutine(this ICore core)
        {
            return rectDrawRoutine;
        }

        public static IDrawCommand<FillCommandParameter> FillDrawRoutine(this ICore core)
        {
            return fillDrawRoutine;
        }
    }

    public static class UndoDrawRoutines
    {
        internal static readonly IUndo<LineCommandParameter> lineDrawRoutine =
            new LineUndo();

        internal static readonly IUndo<RectCommandParameter> rectDrawRoutine =
            null;

        internal static readonly IUndo<FillCommandParameter> fillDrawRoutine =
            null;

        public static IUndo<LineCommandParameter> LineDrawRoutine(this ICore core)
        {
            return lineDrawRoutine;
        }

        public static IUndo<RectCommandParameter> RectDrawRoutine(this ICore core)
        {
            return rectDrawRoutine;
        }

        public static IUndo<FillCommandParameter> FillDrawRoutine(this ICore core)
        {
            return fillDrawRoutine;
        }
    }
}