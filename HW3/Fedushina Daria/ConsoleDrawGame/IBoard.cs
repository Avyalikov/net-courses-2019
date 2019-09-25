using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDrawGame
{
    interface IBoard
    {
        int boardSizeX { get; set; }
        int boardSizeY { get; set; }
        int OX { get; }
        int OY { get; }
       int FigOX { get; set; }
       int FigOY { get; set; }
        void WriteAt(string symbol, int x, int y);

        void DrawBoard(IBoard board);


    }
}
