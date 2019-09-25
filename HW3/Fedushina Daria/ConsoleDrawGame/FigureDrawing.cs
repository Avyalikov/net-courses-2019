using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDrawGame
{
    class FigureDrawing : IFigureDrawing
    {
        ConsoleIO ioDevice = new ConsoleIO();
        public void DrawDot(IBoard board)
        {
            board.FigOX = board.OX;           //define an origin dot for this figure
            board.FigOY = board.OY;
            board.WriteAt(".", board.boardSizeX / 8, board.boardSizeY / 2);
        }

        public void DrawVerticalLine(IBoard board)
        {
            board.FigOX = (board.boardSizeX / 8) * 3;           //define an origin dot for this figure
            board.FigOY = board.OY;
            for (int i = 2; i < board.boardSizeY-2; i++)
            {
                board.WriteAt("|", 0, i);
            }
        }

        public void DrawHorisontalLine(IBoard board)
        {
            board.FigOX = board.boardSizeX / 2;
            board.FigOY = board.OY;
            for (int i = 0; i < board.boardSizeX / 4 - 2; i++)
            {
                board.WriteAt("_", i, board.boardSizeY / 2);
            }

        }

        public void DrawSquare(IBoard board)
        {
            board.FigOX = (board.boardSizeX / 8) * 7;
            board.FigOY = board.OY+(board.boardSizeY/ 2 - board.boardSizeY / 8);
            int height = board.boardSizeY/4 ;                               //define a height of squear
            int width = board.boardSizeX/8;                                 //define a width of squear
            board.WriteAt("+", 0, 0);
            // Draw the left side of a 5x5 rectangle, from top to bottom.
            for (int i = 1; i < height; i++)
            {
                board.WriteAt("|", 0, i);
            }
            board.WriteAt("+", 0, height);


            // Draw the bottom side, from left to right.
            for (int i = 1; i < width; i++)
            {
                board.WriteAt("-", i, height);   // shortcut: WriteAt("---", 1, 4)
            }
            board.WriteAt("+", width, height);

            // Draw the right side, from bottom to top.
            for (int i = height-1; i > 0; i--)
            {
                board.WriteAt("|", width, i);
            }
            board.WriteAt("+", width, 0);

            // Draw the top side, from right to left.
            for (int i = width-1; i > 0; i--)
            {
                board.WriteAt("-", i, 0);   // shortcut: WriteAt("---", 1, 4)
            }

        }
    }
}
