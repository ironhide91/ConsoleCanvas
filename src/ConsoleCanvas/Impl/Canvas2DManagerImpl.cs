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
                Render();
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
            Logger.Info("Rendering ...");
            Render();
            Logger.Info("Rendered");
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
            Render();
            console.Write($"Actual DrawUnits [{canvas.Get().Count}] Canvas [{currentDimension.Width * currentDimension.Height}]");
            console.NewLine();
        }

        private void Undo(ICommandExecutor executor, IDrawRoutineParam param, IEnumerable<Point2D> previousCommandPoints)
        {            
            Logger.Info("Reverting ...");
            executor.Undo(canvas, param, previousCommandPoints);
            Logger.Info("Reverted");
            Logger.Info("Rendering ...");
            Render();
            Logger.Info("Rendered");
            console.Write($"Actual DrawUnits [{canvas.Get().Count}] Canvas [{currentDimension.Width * currentDimension.Height}]");
            console.NewLine();
        }

        private void Render()
        {
            Logger.Info("Rendering ...");            
            console.NewLine();
            console.Write($"Canvas [{currentDimension.Width} X {currentDimension.Height}]");
            console.NewLine();
            console.NewLine();

            console.Write("|  ");
            int xindex = 1;
            for (int i = 0; i < CurrentDimension.Width; i++)
            {
                console.Write(string.Format("|{0:00}", xindex++));
            }
            console.Write("|");
            console.NewLine();

            var unitsToDraw = canvas.Get();

            for (int y = 1; y <= currentDimension.Height; y++)
            {
                console.Write(string.Format("|{0:00}|", y));

                for (int x = 1; x < currentDimension.Width; x++)
                {
                    var point = new Point2D(x, y);

                    if (unitsToDraw.ContainsKey(point))
                    {
                        console.Write(" ");
                        console.Write(unitsToDraw[point]);
                        console.Write(" ");
                        continue;
                    }

                    console.Write("   ");
                }

                var lastPointInRow = new Point2D(currentDimension.Width, y);
                if (unitsToDraw.ContainsKey(lastPointInRow))
                {
                    console.Write(" ");
                    console.Write(unitsToDraw[lastPointInRow]);
                }
                else
                {
                    console.Write("  ");
                }

                console.Write("|");
                console.NewLine();
            }

            console.NewLine();
            Logger.Info("Rendered");
        }

        private void Close()
        {
            canvas.Reset();
        }
        #endregion
    }
}