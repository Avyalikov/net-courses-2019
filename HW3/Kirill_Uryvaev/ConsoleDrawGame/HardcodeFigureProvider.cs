using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDrawGame
{
    class HardcodeFigureProvider : IFigureProvider
    {
        public Dictionary<string,Draw> GetFigures()
        {
            Dictionary<string, Draw> figures = new Dictionary<string, Draw>();
            figures.Add("Dot", DrawDot);
            figures.Add("Hline", DrawHorizontalLine);
            figures.Add("Vline", DrawVerticalLine);
            figures.Add("Something", DrawSomething);
            return figures;
        }

        // Default board considered as 10x10

        void DrawDot(IBoard board)
        {
            board.DrawAt('.',2,2);
        }

        void DrawVerticalLine(IBoard board)
        {
            for (int i=1;i<10;i++)
            {
                board.DrawAt('|', 3, i);
            }         
        }

        void DrawHorizontalLine(IBoard board)
        {
            for (int i = 1; i < 10; i++)
            {
                board.DrawAt('-', i, 3);
            }
        }
        void DrawSomething(IBoard board)
        {
            board.DrawAt('@', 1, 1);
            board.DrawAt('@', 10, 1);
        }
    }

}
