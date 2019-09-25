using Doors_and_levels_game.Interfaces;
using System;
using System.Collections.Generic;

namespace Doors_and_levels_game.Components
{
    internal class ConsoleIOModule : InputOutputModule
    {
        public override string Input() => Console.ReadLine();
        public override void Print(string str = "", string end = "\n") => Console.Write($"{str}{end}");
    }
}