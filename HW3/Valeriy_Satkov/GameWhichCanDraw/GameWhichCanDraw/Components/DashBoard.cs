// <copyright file="DashBoard.cs" company="Valeriy Satkov">
// All rights reserved.
// </copyright>
// <author>Valeriy Satkov</author>

namespace GameWhichCanDraw.Components
{
    using System;

    /// <summary>
    /// Board class - area of output
    /// </summary>
    internal class DashBoard : Interfaces.IBoard
    {
        /// <summary>
        /// Angle symbol
        /// </summary>
        private readonly char angle = '+';

        /// <summary>
        /// Vertical line symbol
        /// </summary>
        private readonly char vertical = '|';

        /// <summary>
        /// Horizontal line symbol
        /// </summary>
        private readonly char horizontal = '-';

        /// <summary>
        /// X (left) start position
        /// </summary>
        private int origRow;

        /// <summary>
        /// Y (top) start position
        /// </summary>
        private int origCol;

        /// <summary>
        /// Gets or sets the Length of board
        /// </summary>
        public int BoardSizeX { get; set; }

        /// <summary>
        /// Gets or sets the Width of board
        /// </summary>
        public int BoardSizeY { get; set; }

        /// <summary>
        /// Create the board
        /// </summary>
        public virtual void Create()
        {
            // Console.Clear();
            this.origCol = Console.CursorLeft; // save original left coordinate
            this.origRow = Console.CursorTop; // save original top coordinate            

            // Draw the angles
            this.WriteAt(this.angle, 0, 0);
            this.WriteAt(this.angle, 0, this.BoardSizeY - 1);
            this.WriteAt(this.angle, this.BoardSizeX - 1, 0);
            this.WriteAt(this.angle, this.BoardSizeX - 1, this.BoardSizeY - 1);

            for (int i = 1; i < this.BoardSizeX - 1; i++)
            {
                this.WriteAt(this.horizontal, i, 0);         // Draw the top side, from right to left.
                this.WriteAt(this.horizontal, i, this.BoardSizeY - 1); // Draw the bottom side, from left to right.                
            }

            for (int i = 1; i < this.BoardSizeY - 1; i++)
            {
                this.WriteAt(this.vertical, 0, i);           // Draw the left side of a 5x5 rectangle, from top to bottom.
                this.WriteAt(this.vertical, this.BoardSizeX - 1, i);  // Draw the right side, from bottom to top.
            }

            // this.WriteAt('\r', 0, this.BoardSizeY);
        }

        /// <summary>
        /// Write a symbol on board
        /// </summary>
        /// <param name="c">Symbol to print</param>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public virtual void WriteAt(char c, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(this.origCol + x, this.origRow + y);
                Console.Write(c);
            }
            catch (ArgumentException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
    }
}
