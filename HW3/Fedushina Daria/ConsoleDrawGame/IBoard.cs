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

        int OrigX { get; set; }
        int OrigY { get; set; }

        void WriteAt(string symbol, int x, int y);

        void Draw(IBoard board);


    }
}
