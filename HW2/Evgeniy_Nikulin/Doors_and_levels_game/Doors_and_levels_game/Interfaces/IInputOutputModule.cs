using System.Collections;
using System.Collections.Generic;

namespace Doors_and_levels_game.Interfaces
{
    public abstract class InputOutputModule
    {
        public abstract string Input();
        public abstract void Print(string str = "", string end = "\n");
        public void Print(IList arr, string sep = " ", string end = "\n")
        {
            bool isFirst = true;
            foreach (var item in arr)
            {
            Print($"{(isFirst ? "" : sep)}{item}", end: "");
                isFirst = false;
            }
            Print(end: end);
        }
    }
}