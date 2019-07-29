using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWhichCanDraw
{
    class DrawOnConsoleBoard: IDrawOnBoard
    {
        public void DrawSimpleDot(IBoard board)
        {
            board.WriteAt(".", board.boardSizeX / 2, board.boardSizeY / 4);
        }
        public void DrawHorizontalLine(IBoard board)
        {
            for(int i = 1; i < board.boardSizeX; i++)
            {
                board.WriteAt("-", i, board.boardSizeY/2);
            }
        }

        public void DrawVerticalLine(IBoard board)
        {
            for (int i = 1; i < board.boardSizeY; i++)
            {
                board.WriteAt("|", board.boardSizeX/2, i);
            }
        }

        public void DrawCircle(IBoard board)
        {
            int radius;
            if(board.boardSizeX < board.boardSizeY)
            {
                radius = board.boardSizeX / 4;
            }
            else
            {
                radius = board.boardSizeY / 4;
            }

            int x0 = board.boardSizeX / 2;
            int y0 = board.boardSizeY / 2;

            for (int alpha = 0; alpha < 360; alpha ++)
            {
                board.WriteAt("*", Convert.ToInt32(radius * Math.Cos(alpha))+ x0, Convert.ToInt32(radius * Math.Sin(alpha)) + y0);
            }
        }

        public void CleanBoard (IBoard board)
        {
            Console.Clear();
        }
    }
}
