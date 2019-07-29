namespace ConsoleDraw.Provider
{
    using ConsoleDraw.Interfaces;
    using System;

    public class FigureProvider : IFigureProvider
    {
        public void Curve(IBoard board)
        {
            for (int i = 0; i < board.BoardSizeX - 1; i++)
            {
                int func;
                if ((func = board.BoardSizeY - (int)Math.Pow(i, 2)) < 1) 
                    break;

                board.WriteAt('*', i, func);
            }
        }

        public void HorizontalLine(IBoard board)
        {
            for(int i = 0; i < board.BoardSizeX - 1; i++)
            {
                board.WriteAt('-', i, (board.BoardSizeY / 2) + 2);
            }
        }

        public void SimpleDot(IBoard board)
        {
            board.WriteAt('.', board.BoardSizeX / 2, board.BoardSizeY / 2);
        }

        public void VerticalLine(IBoard board)
        {
            for (int i = 0; i < board.BoardSizeY - 1; i++)
            {
                board.WriteAt('|', (board.BoardSizeX / 2) + 2, i);
            }
        }
    }
}
