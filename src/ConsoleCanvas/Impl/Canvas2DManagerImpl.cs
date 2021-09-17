using ConsoleCanvas.Core;
using ConsoleCanvas.Impl.Parameter;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleCanvas.Impl
{
    public class Canvas2DManagerImpl : ICanvas2DManager
    {
        internal Canvas2DManagerImpl()
        {
            maxDimension = Settings.MaxDimension;
        }

        #region Private Fields
        private readonly Dimension2D maxDimension;

        private IReadOnlyDictionary<string, ICommandExecutor> commandExecutors;
        private IConsole console;
        private ICanvas2D canvas;
        private IRender renderer;
        private IDrawCanvasCommand drawCanvasCommand;
        private IUndoPreviousCommand undoPreviousCommand;

        private Dimension2D currentDimension;
        private bool canvasInitialized = false;        
        private HashSet<string> knownKeys = new HashSet<string>(10);
        #endregion

        internal IReadOnlySet<string> KnownKeys { get { return knownKeys; } }
        internal ICommandIdentifier CommandIdentifier { get; private set; }

        #region Builder Methods
        internal void SetConsole(IConsole console)
        {
            this.console = console;
        }

        internal void SetCanvas2D(ICanvas2D canvas)
        {
            this.canvas = canvas;
        }

        internal void SetCommandIdentifier(ICommandIdentifier commandIdentifier)
        {
            CommandIdentifier = commandIdentifier;
        }

        internal void SetCanvasDrawCommand(IDrawCanvasCommand command)
        {
            drawCanvasCommand = command;
        }

        internal void SetRenderer(IRender renderer)
        {
            this.renderer = renderer;
        }

        internal void SetUndo(IUndoPreviousCommand undoPreviousCommand)
        {
            this.undoPreviousCommand = undoPreviousCommand;
        }

        internal void SetCommandExecutors(IReadOnlyDictionary<string, ICommandExecutor> commandExecutors)
        {
            this.commandExecutors = commandExecutors;
        }

        internal void BuildKnowKeys()
        {
            knownKeys.Add(drawCanvasCommand.CommandKey);
            knownKeys.Add(Constants.UndoKey);

            foreach (var key in commandExecutors.Keys.Select(k => k))
                knownKeys.Add(key);
        }
        #endregion

        #region ICanvas2DManager
        public Dimension2D MaxDimension
        {
            get { return maxDimension; }
        }

        public Dimension2D CurrentDimension
        {
            get { return currentDimension; }
        }

        public void Initialize(Dimension2D dimension)
        {
            if (canvas.Initialize(dimension))
            {
                currentDimension = dimension;
                canvasInitialized = true;
                return;
            }

            canvasInitialized = false;
        }

        public void Run()
        {
            Logger.Info("Now running !");

            console.NewLine();

            while (true)
            {                
                var input = console.ReadLine();
                Logger.Info($"Input receieved - {input}");

                if (!string.IsNullOrEmpty(input) && (input.Trim() == Constants.QuitKey))
                {
                    Logger.Info("Receieved Quit command");
                    Logger.Info("Quiting ...");
                    Logger.Info("Quit");
                    Close();
                    break;
                }

                ProcessCommand(input);
                console.NewLine();
            }
        }
        #endregion

        #region Private Methods
        private void ProcessCommand(string command)
        {
            var key = CommandIdentifier.Identify(command);

            if (key == null)
            {
                Logger.Info("Receieved null command");
                return;
            }

            if (key == drawCanvasCommand.CommandKey)
            {
                Canvas(command);
                return;
            }

            if (!canvasInitialized)
            {
                Logger.Error("Canvas uninitialized");
                return;
            }

            if (key == Constants.UndoKey)
            {
                undoPreviousCommand.Undo(canvas, commandExecutors);
                renderer.Render(canvas, console);
                return;
            }

            if (commandExecutors.ContainsKey(key))
            {
                Draw(commandExecutors[key], command);
                return;
            }
        }

        private void Canvas(string command)
        {
            Logger.Info("Receieved potential Canvas command");

            var success = drawCanvasCommand.Parser.TryParse(command, out CanvasCommandParameter parsedCommand);
            if (!success)
            {
                Logger.Error("Command parsing failed");
                return;
            }

            success = drawCanvasCommand.Validator.IsValid(parsedCommand, maxDimension);
            if (!success)
            {
                Logger.Error("Invalid command");
                return;
            }

            Logger.Info("Receieved a valid Canvas command");
            Logger.Info("Drawing ...");
            drawCanvasCommand.Draw(this, parsedCommand.Dimension);
            Logger.Info("Drawn");
            renderer.Render(canvas, console);
            console.Write($"Actual DrawUnits [{canvas.Get().Count}] Canvas [{currentDimension.Width * currentDimension.Height}]");
            console.NewLine();
        }

        private void Draw(ICommandExecutor executor, string command)
        {
            var result = executor.Execute(canvas, currentDimension, command);

            if (!result.Item1)
                return;
            
            undoPreviousCommand.Record(result.Item2);
            Logger.Info("Drawing ...");
            canvas.Draw(result.Item3);
            result.Item2.AssociatedPoints = result.Item3.Select(s => s.Coordinate).ToList();
            Logger.Info("Drawn");
            renderer.Render(canvas, console);
            console.Write($"Actual DrawUnits [{canvas.Get().Count}] Canvas [{currentDimension.Width * currentDimension.Height}]");
            console.NewLine();
        }

        private void Close()
        {
            canvas.Reset();
        }
        #endregion
    }
}