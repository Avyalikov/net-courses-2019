// <copyright file="DrawOnConsoleBoard.cs" company="Epam">
// Copyright (c) Epam. All rights reserved.
// </copyright>

namespace GameWhichCanDraw
{
    using System;

    /// <summary>
    /// Class with draw on console board methods
    /// </summary>
    public class DrawOnConsoleBoard : IDrawOnBoard
    {
        /// <summary>
        /// Draws simple dot in console
        /// </summary>
        /// <param name="board">Board component</param>
        public void DrawSimpleDot(IBoard board)
        {
            board.WriteAt(".", board.boardSizeX / 2, board.boardSizeY / 4);
        }

        /// <summary>
        /// Draws horizontal line in console
        /// </summary>
        /// <param name="board">Board component</param>
        public void DrawHorizontalLine(IBoard board)
        {
            for (int i = 1; i < board.boardSizeX; i++)
            {
                board.WriteAt("-", i, board.boardSizeY / 2);
            }
        }

        /// <summary>
        /// Draws vertical line in console
        /// </summary>
        /// <param name="board">Board component</param>
        public void DrawVerticalLine(IBoard board)
        {
            for (int i = 1; i < board.boardSizeY; i++)
            {
                board.WriteAt("|", board.boardSizeX / 2, i);
            }
        }

        /// <summary>
        /// Draws circle in console
        /// </summary>
        /// <param name="board">Board component</param>
        public void DrawCircle(IBoard board)
        {
            int radius;
            if (board.boardSizeX < board.boardSizeY)
            {
                radius = board.boardSizeX / 4;
            }
            else
            {
                radius = board.boardSizeY / 4;
            }

            int x0 = board.boardSizeX / 2;
            int y0 = board.boardSizeY / 2;

            for (int alpha = 0; alpha < 360; alpha++)
            {
                board.WriteAt("*", Convert.ToInt32(radius * Math.Cos(alpha)) + x0, Convert.ToInt32(radius * Math.Sin(alpha)) + y0);
            }
        }

        /// <summary>
        /// Cleans console board
        /// </summary>
        /// <param name="board">Board component</param>
        public void CleanBoard (IBoard board)
        {
            Console.Clear();
        }
    }
}
