using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDrawGame
{
    interface IBoard
    {
        void DrawAt(char symbol, int x, int y);
        void DrawBoard(IBoard board);
        void SetBoardSize(int width, int heigh);
        void Clear();
    }
}
