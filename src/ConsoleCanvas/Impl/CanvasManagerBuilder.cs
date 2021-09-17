using ConsoleCanvas.Core;
using ConsoleCanvas.Impl.Executor;
using ConsoleCanvas.Impl.Parser;
using ConsoleCanvas.Impl.Validator;
using ConsoleCanvas.Impl.DrawRoutine;
using ConsoleCanvas.Impl.Undo;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ConsoleCanvas.Impl
{
    public sealed class CanvasManagerBuilder
    {
        public CanvasManagerBuilder()
        {
            manager.SetCommandIdentifier(new CommandIdentifier());

            SetConsole(new ConsoleImpl());
            SetRenderer(new Renderer());
            SetCanvas2D(new Canvas2D(' ', 'X', 0, 7, new Point2DComparer()));

            manager.SetUndo(new UndoPreviousCommand());

            WithCanvasDrawCommand(
                new CanvasCommandExecutor(
                    new CanvasCommandParser(),
                    new CanvasCommandValidator())
            );

            WithCanvasDrawCommand(
                new CanvasCommandExecutor(
                    new CanvasCommandParser(),
                    new CanvasCommandValidator())
            );

            RegisterCommandExecutor(
                new LineCommandExecutor(
                    new LineCommandParser(),
                    new LineCommandValidator(),
                    new LineDrawer(),
                    new LineUndo())
            );

            RegisterCommandExecutor(
                new RectCommandExecutor(
                    new RectCommandParser(),
                    new RectCommandValidator(),
                    new RectDrawer(),
                    new RectUndo())
            );

            RegisterCommandExecutor(
                new FillCommandExecutor(
                    new FillCommandParser(),
                    new FillCommandValidator(),
                    new FillDrawer(),
                    new FillUndo())
            );
        }

        private readonly Canvas2DManagerImpl manager = new Canvas2DManagerImpl();

        private readonly Dictionary<string, ICommandExecutor> commandExecutors =
            new Dictionary<string, ICommandExecutor>();

        public ICanvas2DManager Build()
        {
            manager.SetCommandExecutors(new ReadOnlyDictionary<string, ICommandExecutor>(commandExecutors));
            manager.BuildKnowKeys();
            manager.CommandIdentifier.BuildIdentifiers(manager.KnownKeys);
            return manager;
        }

        public CanvasManagerBuilder SetConsole(IConsole console)
        {
            manager.SetConsole(console);
            return this;
        }
        
        public CanvasManagerBuilder SetCanvas2D(ICanvas2D canvas)
        {
            manager.SetCanvas2D(canvas);
            return this;
        }

        public CanvasManagerBuilder SetRenderer(IRender renderer)
        {
            manager.SetRenderer(renderer);
            return this;
        }

        public CanvasManagerBuilder WithCanvasDrawCommand(IDrawCanvasCommand draw)
        {
            manager.SetCanvasDrawCommand(draw);
            return this;
        }

        public CanvasManagerBuilder RegisterCommandExecutor(ICommandExecutor executor)
        {
            if (commandExecutors.ContainsKey(executor.CommandKey))
            {
                commandExecutors[executor.CommandKey] = executor;
                return this;
            }

            commandExecutors.Add(executor.CommandKey, executor);
            return this;
        }        
    }
}