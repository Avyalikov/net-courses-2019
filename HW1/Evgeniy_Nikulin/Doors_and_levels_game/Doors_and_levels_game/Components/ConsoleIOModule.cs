using Doors_and_levels_game.Interfaces;
using System;
using System.Collections.Generic;

namespace Doors_and_levels_game.Components
{
    internal class ConsoleIOModule : IInputOutputModule
    {
        public string Input() => Console.ReadLine();
        public void Print<T>(T[] arr, string sep = " ", string end = "\n")
        {
            bool isFirst = true;
            foreach (T item in arr)
            {                
                Console.Write($"{(isFirst ? "" : sep)}{item}");
                isFirst = false;
            }
            Console.Write(end);
        }
        public void Print<T>(List<T> list, string sep = " ", string end = "\n") => Print(list.ToArray(), sep, end);
        public void Print(string str = "", string end = "\n") => Console.Write($"{str}{end}");
    }
}