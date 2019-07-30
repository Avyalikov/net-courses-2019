//-----------------------------------------------------------------------
// <copyright file="Drawer.cs" company="AVLozhechkin">
//     Copyright (c) AVLozhechkin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DashboardGame
{
    using System;

    /// <summary>
    /// This static class Contains methods for plotting different figures.
    /// </summary>
    internal static class Drawer
    {
        /// <summary>
        /// Gets or sets Random instance which is used for plotting.
        /// </summary>
        private static Random Rand { get; set; } = new Random();

        /// <summary>
        /// This method draws a red point with a random coordinates on board.
        /// </summary>
        /// <param name="board">IBoard for drawing on it.</param>
        public static void DrawPoint(IBoard board)
        {
            int x = Rand.Next((-board.BoardSizeX / 2) + 2, (board.BoardSizeX / 2) - 3);
            int y = Rand.Next((-board.BoardSizeY / 2) + 2, (board.BoardSizeY / 2) - 1);

            board.SetColor(ConsoleColor.Red);
            board.DrawAtPosition(x, y, "•");
            board.ResetColor();
        }

        /// <summary>
        /// This method draws a yellow vertical line with a random x-coordinate on board.
        /// </summary>
        /// <param name="board">IBoard for drawing on it.</param>
        public static void DrawVerticalLine(IBoard board)
        {
            int x = Rand.Next((-board.BoardSizeX / 2) + 1, (board.BoardSizeX / 2) - 3);
            board.SetColor(ConsoleColor.Yellow);

            for (int i = (-board.BoardSizeY / 2) + 2; i < (board.BoardSizeY / 2); i++)
            {
                board.DrawAtPosition(x, i, "|");
            }

            board.ResetColor();
        }

        /// <summary>
        /// This method draws a cyan horizontal line with a random y-coordinate on board.
        /// </summary>
        /// <param name="board">IBoard for drawing on it.</param>
        public static void DrawHorizontalLine(IBoard board)
        {
            int y = Rand.Next((-board.BoardSizeY / 2) + 2, (board.BoardSizeY / 2) - 1);
            board.SetColor(ConsoleColor.Cyan);

            for (int i = (-board.BoardSizeX / 2) + 1; i < (board.BoardSizeX / 2) - 2; i++)
            {
                board.DrawAtPosition(i, y, "―");
            }

            board.ResetColor();
        }

        /// <summary>
        /// This method draws a dark magenta parabola on board.
        /// </summary>
        /// <param name="board">IBoard for drawing on it.</param>
        public static void DrawParabola(IBoard board)
        {
            board.SetColor(ConsoleColor.DarkMagenta);

            for (int i = (-board.BoardSizeY / 2) + 1; i < (board.BoardSizeY / 2) - 3; i++)
            {
                int j = Parabola(i);
                if (j < (board.BoardSizeY / 2))
                {
                    board.DrawAtPosition(i, j, "•");
                }
            }

            board.ResetColor();
        }

        /// <summary>
        /// This method is used for calculating parabola's points.
        /// </summary>
        /// <param name="x">Parabola's x coordinate.</param>
        /// <returns>Parabola's y coordinate.</returns>
        private static int Parabola(int x) => ((x * x) / 4);
    }
}
