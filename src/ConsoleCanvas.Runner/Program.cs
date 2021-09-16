﻿using ConsoleCanvas.Core;
using ConsoleCanvas.Impl;
using System;

namespace ConsoleCanvas.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new CanvasManagerBuilder();
            ICanvas2DManager canvasManager = builder.Build();
            canvasManager.Run();
            Console.ReadLine();
        }
    }
}