using System;
using System.Text;

namespace DashboardGame
{
    class ConsoleBoard : IBoard
    {
        private readonly int boardHeight;
        private readonly int boardWidth;
        private readonly ConsoleColor defaultConsoleColor;
        public ConsoleBoard()
        {
            boardHeight = 40;
            boardWidth = 100;
            Console.SetWindowSize(boardWidth, boardHeight);
            Console.BufferHeight = boardHeight;
            Console.BufferWidth = boardWidth;
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
            defaultConsoleColor = Console.ForegroundColor;
        }

        public void DrawBoard()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < boardWidth - 2; i++)
            {
                DrawAtPosition(-boardWidth / 2 + i, -(boardHeight / 2 - 1), "―");
                DrawAtPosition(-boardWidth / 2 + i, boardHeight / 2, "―");
            }
            for (int i = 0; i < boardHeight - 1; i++)
            {
                DrawAtPosition(-boardWidth / 2, boardHeight / 2 - i, "|");
                DrawAtPosition(boardWidth / 2 - 2, boardHeight / 2 - i, "|");
            }
            DrawAtPosition(boardWidth / 2 - 2, -boardHeight / 2 + 1, "+");
            DrawAtPosition(-boardWidth / 2, boardHeight / 2, "+");
            DrawAtPosition(-boardWidth / 2, -boardHeight / 2 + 1, "+");
            DrawAtPosition(boardWidth / 2 - 2, boardHeight / 2, "+");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void DrawAtPosition(int x, int y, string s)
        {
            Console.SetCursorPosition(boardWidth / 2 + x, boardHeight / 2 - y);
            Console.Write(s);
        }
        public string ReadLine() => Console.ReadLine();
        public void DrawAxis()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = -boardHeight / 2 + 2; i < boardHeight / 2; i++)
            {
                DrawAtPosition(0, i, "|");
            }
            for (int i = -boardWidth / 2 + 1; i < boardWidth / 2 - 2; i++)
            {
                DrawAtPosition(i, 0, "―");
            }
            DrawAtPosition(0, 0, "+");
            DrawAtPosition(1, 1, "0");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public int GetHeight()
        {
            return boardHeight;
        }

        public int GetWidth()
        {
            return boardWidth;
        }

        public void SetColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }
        public void ResetColor()
        {
            Console.ForegroundColor = defaultConsoleColor;
        }

        public void Clear()
        {
            Console.Clear();
            DrawBoard();
        }
    }
}
