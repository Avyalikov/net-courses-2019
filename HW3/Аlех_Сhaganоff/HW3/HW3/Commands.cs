namespace HW3
{
    using System;

    public class Commands : ICommands
    {
        public void DrawDashboard(IBoard board)
        {
            Console.Clear();

            for (int i = 0; i < board.BoardSizeX; ++i)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("X");
            }

            for (int i = 0; i < board.BoardSizeY; ++i)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("X");

                Console.SetCursorPosition(board.BoardSizeX - 1, i);
                Console.Write("X");
            }

            for (int i = 0; i < board.BoardSizeX; ++i)
            {
                Console.SetCursorPosition(i, board.BoardSizeY - 1);
                Console.Write("X");
            }

            Console.WriteLine();
            Console.WriteLine();
        }

        public void DrawDot(IBoard board)
        {
            Console.SetCursorPosition(board.BoardSizeX / 2, board.BoardSizeY / 2);
            Console.Write("X");
            Console.SetCursorPosition(0, board.BoardSizeY + 1);
        }

        public void DrawHorizontalLine(IBoard board)
        {
            for (int i = 0; i < board.BoardSizeX; ++i)
            {
                Console.SetCursorPosition(i, board.BoardSizeY / 2);
                Console.Write("X");
                Console.SetCursorPosition(0, board.BoardSizeY + 1);
            }
        }

        public void DrawVerticalLine(IBoard board)
        {
            for (int i = 0; i < board.BoardSizeY; ++i)
            {
                Console.SetCursorPosition(board.BoardSizeX / 2, i);
                Console.Write("X");
                Console.SetCursorPosition(0, board.BoardSizeY + 1);
            }
        }

        public void DrawSnowFlake(IBoard board)
        {
            for (int i = (board.BoardSizeX / 2) - 3; i < (board.BoardSizeX / 2) + 4; ++i)
            {
                Console.SetCursorPosition(i, board.BoardSizeY / 2);
                Console.Write("X");    
            }

            for (int i = (board.BoardSizeY / 2) - 3; i < (board.BoardSizeY / 2) + 4; ++i)
            {
                Console.SetCursorPosition(board.BoardSizeX / 2, i);
                Console.Write("X");
            }

            for (int x = (board.BoardSizeX / 2) - 3, y = (board.BoardSizeY / 2) - 3; x < (board.BoardSizeX / 2) + 4; ++x, ++y)
            {
                Console.SetCursorPosition(x, y);
                Console.Write("X");
            }

            for (int x = (board.BoardSizeX / 2) - 3, y = (board.BoardSizeY / 2) + 3; x < (board.BoardSizeX / 2) + 4; ++x, --y)
            {
                Console.SetCursorPosition(x, y);
                Console.Write("X");
            }

            Console.SetCursorPosition(0, board.BoardSizeY + 1);
        }
    }
}
