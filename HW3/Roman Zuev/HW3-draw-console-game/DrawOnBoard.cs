// <copyright file="DrawOnBoard.cs" company=".net courses 2019">
// All rights reserved.
// </copyright>
// <author>Roman Zuev</author>

namespace HW3_console_draw_game
{
    using System;

    /// <summary>
    /// Basic game logic class
    /// </summary>
    internal class DrawOnBoard : IDrawOnBoard
    {
        /// <summary>
        /// The DrawBoard
        /// </summary>
        /// <param name="board">The board<see cref="IBoard"/></param>
        public void DrawBoard(IBoard board)
        {
            this.WriteAt("+", 0, 0);
            for (int i = 1; i < board.BoardSizeX; i++)
            {
                this.WriteAt("-", i, 0);
            }

            this.WriteAt("+", board.BoardSizeX, 0);
            for (int i = 1; i < board.BoardSizeY; i++)
            {
                this.WriteAt("|", board.BoardSizeX, i);
            }

            this.WriteAt("+", board.BoardSizeX, board.BoardSizeY);
            for (int i = board.BoardSizeX - 1; i > 0; i--)
            {
                this.WriteAt("-", i, board.BoardSizeY);
            }

            this.WriteAt("+", 0, board.BoardSizeY);
            for (int i = board.BoardSizeY - 1; i > 0; i--)
            {
                this.WriteAt("|", 0, i);
            }

            Console.SetCursorPosition(0, board.BoardSizeY + 2);
        }

        /// <summary>
        /// The DrawHorizontalLine
        /// </summary>
        /// <param name="board">The board<see cref="IBoard"/></param>
        public void DrawHorizontalLine(IBoard board)
        {
            for (int i = 1; i < board.BoardSizeX; i++)
            {
                this.WriteAt("-", i, board.BoardSizeY / 2);
            }

            Console.SetCursorPosition(0, board.BoardSizeY + 2);
        }

        /// <summary>
        /// The DrawSimpleDot
        /// </summary>
        /// <param name="board">The board<see cref="IBoard"/></param>
        public void DrawSimpleDot(IBoard board)
        {
            this.WriteAt(".", board.BoardSizeX / 2, board.BoardSizeY / 2);
            Console.SetCursorPosition(0, board.BoardSizeY + 2);
        }

        /// <summary>
        /// The DrawVerticalLine
        /// </summary>
        /// <param name="board">The board<see cref="IBoard"/></param>
        public void DrawVerticalLine(IBoard board)
        {
            for (int i = 1; i < board.BoardSizeY; i++)
            {
                this.WriteAt("|", board.BoardSizeX / 2, i);
            }

            Console.SetCursorPosition(0, board.BoardSizeY + 2);
        }

        /// <summary>
        /// The DrawV
        /// </summary>
        /// <param name="board">The board<see cref="IBoard"/></param>
        public void DrawV(IBoard board)
        {
            int x = (board.BoardSizeX / 2) - board.BoardSizeY > 0 ? (board.BoardSizeX / 2) - board.BoardSizeY : (board.BoardSizeX / 2);
            int y = 1;
            while (x < board.BoardSizeX && y < board.BoardSizeY)
            {
                this.WriteAt("*", x, y);
                x++;
                y++;
            }

            while (x < board.BoardSizeX && y > 1)
            {
                x++;
                y--;
                this.WriteAt("*", x, y);
            }

            Console.SetCursorPosition(0, board.BoardSizeY + 2);
        }

        /// <summary>
        /// The ClearConsole
        /// </summary>
        /// <param name="board">The board<see cref="IBoard"/></param>
        public void ClearConsole(IBoard board)
        {
            Console.Clear();
        }

        /// <summary>
        /// The WriteAt
        /// </summary>
        /// <param name="s">The s<see cref="string"/></param>
        /// <param name="x">The x<see cref="int"/></param>
        /// <param name="y">The y<see cref="int"/></param>
        private void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(x, y);
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
