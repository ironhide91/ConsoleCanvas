using ConsoleCanvas.Core;
using ConsoleCanvas.Impl.Executor;
using ConsoleCanvas.Impl.Parser;
using ConsoleCanvas.Impl.Validator;
using ConsoleCanvas.Impl.DrawRoutine;
using ConsoleCanvas.Impl.Undo;

namespace ConsoleCanvas.Impl
{
    public sealed class CanvasManagerBuilder
    {
        public CanvasManagerBuilder()
        {
            manager = new Canvas2DManagerImpl();

            manager.SetCommandIdentifier(new CommandIdentifier());

            WithConsole(new ConsoleImpl());
            WithCanvas2D(new Canvas2D(' ', 'X', 0, 7, new Point2DComparer()));

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
                    new RectDrawer())
            );

            RegisterCommandExecutor(
                new FillCommandExecutor(
                    new FillCommandParser(),
                    new FillCommandValidator(),
                    new FillDrawer())
            );
        }

        private readonly Canvas2DManagerImpl manager;

        public ICanvas2DManager Build()
        {
            manager.BuildKnowKeys();
            manager.CommandIdentifier.BuildIdentifiers(manager.KnownKeys);
            return manager;
        }

        public CanvasManagerBuilder WithConsole(IConsole console)
        {
            manager.SetConsole(console);
            return this;
        }
        
        public CanvasManagerBuilder WithCanvas2D(ICanvas2D canvas)
        {
            manager.SetCanvas2D(canvas);
            return this;
        }        

        public CanvasManagerBuilder WithCanvasDrawCommand(IDrawCanvasCommand draw)
        {
            manager.SetCanvasDrawCommand(draw);
            return this;
        }

        public CanvasManagerBuilder RegisterCommandExecutor(ICommandExecutor executor)
        {
            manager.RegisterCommandExecutor(executor);
            return this;
        }        
    }
}