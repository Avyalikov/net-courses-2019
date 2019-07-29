using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ConsoleDrawGame
{
    class HardcodeFigureProvider : IFigureProvider
    {
        // Default board considered as 10x10

        public void DrawDot(IBoard board)
        {
            board.DrawAt('.', 5, 5);
        }

        public void DrawVerticalLine(IBoard board)
        {
            for (int i=1;i< 10; i++)
            {
                board.DrawAt('|', 5, i);
            }         
        }

        public void DrawHorizontalLine(IBoard board)
        {
            for (int i = 1; i < 10; i++)
            {
                board.DrawAt('-', i, 5);
            }
        }
        public void DrawSinus(IBoard board)
        {
            for (int i = 1; i < 10; i++)
            {
                int y = (int)((Math.Sin(i * 2 * Math.PI / 10) + 1) * (10 - 1) / 2);
                board.DrawAt('@', i, y);
            }
        }
    }

}
