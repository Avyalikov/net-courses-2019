using System;
using System.Collections.Generic;
using System.Text;

namespace Console_Draw_Game.Interfaces
{
    interface IBoard
    {
        int BoardSizeX { get; set; }

        int BoardSizeY { get; set; }

        void Create();

        void Draw(char s, int x, int y);
    }
}
