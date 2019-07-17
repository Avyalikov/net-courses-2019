using System.Collections.Generic;

namespace Doors_and_levels_game.Interfaces
{
    public interface IInputOutputModule
    {
        string Input();
        void Print(string str = "", string end = "\n");
        void Print<T>(T[] arr, string sep = " ", string end = "\n");
        void Print<T>(List<T> list, string sep = " " , string end = "\n");
    }
}