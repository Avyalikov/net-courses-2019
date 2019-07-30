// <copyright file="IBoard.cs" company="Valeriy Satkov">
// All rights reserved.
// </copyright>
// <author>Valeriy Satkov</author>

namespace GameWhichCanDraw.Interfaces
{
    /// <summary>
    /// Board interface - area of output
    /// </summary>
    public interface IBoard
    {
        /// <summary>
        /// Gets or sets the Length of board
        /// </summary>
        int BoardSizeX { get; set; }

        /// <summary>
        /// Gets or sets the Width of board
        /// </summary>
        int BoardSizeY { get; set; }

        /// <summary>
        /// Create the board
        /// </summary>
        void Create();

        /// <summary>
        /// Write a symbol on board
        /// </summary>
        /// <param name="c">Symbol to print</param>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        void WriteAt(char c, int x, int y);
    }
}
