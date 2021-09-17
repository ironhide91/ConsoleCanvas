
 
# Requirements

1. Visual Studio 2019
2. DotNet5 Framework
3. Acces to public Nuget api
3. Xunit along with Xunit runner for Visual Studio

# Build

Right click on solution and click build soultion.

# Run

1. Set ConsoleCanvas.Runner as startup project.
2. F5 to run. 

# Test

1. Build the project.
2. Run tests using Visual Studio Test Explorer.
 
# Design Decisions / Project Structure

1. Canvas dimension should not exceed Max dimension.
2. All features are defined through abstraction under ConsoleCanvas.Core/*.
3. All features are implemented through abstraction under ConsoleCanvas.Impl/*.
4. You can only create implemention using the CanvasManagerBuilder.
5. CanvasManagerBuilder exposes various methods to hook various implementations.
6. CanvasManagerBuilder will allow you to extend features with new commands using RegisterCommandExecutor method.
 
# Future requirements anticipation
 
1. Need for more drawing commands. This can be achieved using CommandExecutorBase class.
2. Need for undo feature. This can be achieved using IUndoPreviousCommand interrface.

# Features on top of baseline requirements
 
1. Commnads can be case insensitive and can also contain whitespaces.
2. All types of lines can be drawn - horizontal, vertical, diagonal etc.
3. Undo feature provided for Line commmand only.

# Commands

|Command    | Description |
|-----------|-------------|
| C w h |Should create a new canvas of width w and height h.|
| L x1 y1 x2 y2    | Should create a new line from (x1,y1) to (x2,y2). Currently only horizontal or vertical lines are supported. Horizontal and vertical lines will be drawn using the 'x' character.
| R x1 y1 x2 y2 | Should create a new rectangle, whose upper left corner is (x1,y1) and lower right corner is (x2,y2). Horizontal and vertical lines will be drawn using the 'x' character.|
| B x y c | Should fill the entire area connected to (x,y) with "colour" c. The behavior of this is the same as that of the "bucket fill" tool in paint programs. Value "c" should be an integer where 0 <= c <= 15.|
| Q | Should quit the program.|
