using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDrawGame
{
    class ConsoleBoard : IBoard
    {
        protected static int origRow;
        protected static int origCol;
        private const int dashboardOffset = 50;

        private const int defaultBoardSize = 10;

        private int boardSizeX = 10;
        private int boardSizeY = 10;

        public void DrawAt(char symbol, int x, int y)
        {
            float localX = computeLocalCoordinate(x, boardSizeX);
            float localY = computeLocalCoordinate(y, boardSizeY);
            WriteAt(symbol, (int)localX + dashboardOffset, (int)localY);
            WriteAt('\0', 0, 0);
        }

        public void Clear()
        {
            Console.Clear();
        }

        public void SetBoardSize(int width, int heigh)
        {
            boardSizeX = width;
            boardSizeY = heigh;
        }

        private float computeLocalCoordinate(int coordinate, int border)
        {
            float localCoordinate = MathF.Round(((float)border / defaultBoardSize) * coordinate);
            if (localCoordinate>border)
            {
                localCoordinate = border;
            }
            if (localCoordinate <1)
            {
                localCoordinate = 1;
            }
            return localCoordinate;
        }
        // Below is creating of dashboard from here https://msdn.microsoft.com/ru-ru/library/system.console.setcursorposition(v=vs.110).aspx

        public void DrawBoard(IBoard board)
        {
            // Clear the screen, then save the top and left coordinates.
            Console.Clear();
            origCol = Console.CursorLeft;
            origRow = Console.CursorTop;
            // Draw the left side of a 5x5 rectangle, from top to bottom.
            WriteAt('+', dashboardOffset, 0);
            for (int i = 1; i <= boardSizeY; i++)
            {
                WriteAt('|', dashboardOffset, i);
            }
            WriteAt('+', dashboardOffset, boardSizeY + 1);

            // Draw the bottom side, from left to right.
            for (int i = 1; i <= boardSizeX; i++)
            {
                WriteAt('-', dashboardOffset + i, boardSizeY + 1);
            }
            ; // ...
            WriteAt('+', dashboardOffset + boardSizeX + 1, boardSizeY + 1);

            // Draw the right side, from bottom to top
            for (int i = boardSizeY; i >= 1; i--)
            {
                WriteAt('|', dashboardOffset + boardSizeX + 1, i);
            }
            WriteAt('+', dashboardOffset + boardSizeX + 1, 0);

            // Draw the top side, from right to left.
            for (int i = boardSizeX; i >= 1; i--)
            {
                WriteAt('-', dashboardOffset + i, 0);
            }

            Console.WriteLine();
        }

        protected static void WriteAt(char s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
    }
}
