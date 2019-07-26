namespace GameWhichCanDraw.Components
{
    using System;
    using Interfaces;

    class FigureProvider : IFigureProvider
    {
        public void Empty(IBoard board)
        {
        }

        public void Curve(IBoard board)
        {
            for (int i = 1; i < board.BoardSizeX - 1; i++)
            {
                int func = board.BoardSizeY - (int)Math.Pow(i, 2);
                if (func < 1) break;
                board.WriteAt('*', i, func); // Draw the dot
            }
        }

        public void HorizontalLine(IBoard board)
        {
            for (int i = 1; i < board.BoardSizeX - 1; i++)
            {
                board.WriteAt('-', i, board.BoardSizeY / 2 + 2); // Draw the horizontal line, from left to right.                
            }
        }

        public void SimpleDot(IBoard board)
        {
            board.WriteAt('.', board.BoardSizeX / 2, board.BoardSizeY / 2); // Draw the dot
        }

        public void VerticalLine(IBoard board)
        {
            for (int i = 1; i < board.BoardSizeY - 1; i++)
            {
                board.WriteAt('|', board.BoardSizeX / 2 + 2, i); // Draw the vertical line, from up to down
            }
        }
    }
}
