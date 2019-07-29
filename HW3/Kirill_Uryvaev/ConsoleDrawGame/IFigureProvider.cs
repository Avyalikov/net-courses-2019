using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDrawGame
{
    interface IFigureProvider
    {
        void DrawDot(IBoard board);
        void DrawVerticalLine(IBoard board);
        void DrawHorizontalLine(IBoard board);
        void DrawSinus(IBoard board);
    }
}
