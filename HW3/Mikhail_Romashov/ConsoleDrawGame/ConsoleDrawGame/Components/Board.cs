//-----------------------------------------------------------------------
// <copyright file="Board.cs" company="Epam">
//     Copyright (c) Epam. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Components
{
    using System;
    using Interfaces;

    /// <summary>
    /// Class for draw board
    /// </summary>
    public class Board : IBoard
    {
        /// <summary>
        /// Gets or sets length of the board
        /// </summary>
        public int BoardSizeX { get; set; }

        /// <summary>
        /// Gets or sets width of the board
        /// </summary>
        public int BoardSizeY { get; set; }

        /// <summary>
        /// Draw the board
        /// </summary>
        public void Draw()
        {
            this.DrawAt('+', 0, 0);
            this.DrawAt('+', 0 + this.BoardSizeX, 0);
            this.DrawAt('+', 0, this.BoardSizeY);
            this.DrawAt('+', 0 + this.BoardSizeX, this.BoardSizeY);
            for (int i = 1; i < this.BoardSizeX; i++)
            {
                this.DrawAt('-', i, 0);
                this.DrawAt('-', i, this.BoardSizeY);
            }

            for (int i = 1; i < this.BoardSizeY; i++)
            {
                this.DrawAt('|', 0, i);
                this.DrawAt('|', this.BoardSizeX, i);
            }
        }

        /// <summary>
        /// Draw some symbol on the board
        /// </summary>
        /// <param name="symbol">Char to draw</param>
        /// <param name="coordX">Coordinate from left</param>
        /// <param name="coordY">Coordinate from top</param>
        public void DrawAt(char symbol, int coordX, int coordY)
        {
            try
            {
                Console.SetCursorPosition(coordX, coordY);
                Console.Write(symbol);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
    }
}
