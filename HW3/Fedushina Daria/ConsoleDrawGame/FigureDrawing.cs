using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDrawGame
{
    class FigureDrawing : IFigureDrawing
    {
        int OriginalX;
        int OriginalY;
        int BenderCount;
        ConsoleIO ioDevice = new ConsoleIO();
        //(int, int) OrigXY;
        int IFigureDrawing.Bender
        {
            get
            {
                return BenderCount;
            }
            set
            {
                if (value != 0)
                {
                    BenderCount = value;
                }
                else
                {

                }
            }
        }


        public void DrawDot(IBoard board)
        {

            board.WriteAt(".", board.boardSizeX/2, board.boardSizeY/2);
        }

        public void DrawHorisontalLine(IBoard board)
        {
            for (int i= OriginalX+2; i < board.boardSizeX-2; i++)
            {
                board.WriteAt("_", i, board.boardSizeY/2);
            }
            
        }
        public void DrawVerticalLine(IBoard board)
        {
            for (int i = OriginalY+2; i < board.boardSizeY - 2; i++)
            {
                board.WriteAt("|", board.boardSizeX/2, i);
            }
        }

        public void DrawSquare(IBoard board)
        {
            board.boardSizeX /=2;
            board.boardSizeY /= 2;
            OriginalX += 2;
            OriginalY += 2;
            board.WriteAt("+", 0, 0);
            // Draw the left side of a 5x5 rectangle, from top to bottom.
            for (int i = 1; i < board.boardSizeY; i++)
            {
                board.WriteAt("|", 0, i);
            }
            board.WriteAt("+", 0, board.boardSizeY);


            // Draw the bottom side, from left to right.
            for (int i = 1; i < board.boardSizeX; i++)
            {
                board.WriteAt("-", i, board.boardSizeY);   // shortcut: WriteAt("---", 1, 4)
            }
            board.WriteAt("+", board.boardSizeX, board.boardSizeY);

            // Draw the right side, from bottom to top.
            for (int i = board.boardSizeY - 1; i > 0; i--)
            {
                board.WriteAt("|", board.boardSizeX, i);
            }
            board.WriteAt("+", board.boardSizeX, 0);

            // Draw the top side, from right to left.
            for (int i = board.boardSizeX - 1; i > 0; i--)
            {
                board.WriteAt("-", i, 0);   // shortcut: WriteAt("---", 1, 4)
            }

        }
    }
}
