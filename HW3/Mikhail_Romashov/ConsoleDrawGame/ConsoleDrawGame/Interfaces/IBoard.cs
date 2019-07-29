//-----------------------------------------------------------------------
// <copyright file="IBoard.cs" company="Epam">
//     Copyright (c) Epam. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Interfaces
{
    /// <summary>
    /// Interface for draw board
    /// </summary>
    public interface IBoard
    {
        /// <summary>
        /// Gets or sets width of the board
        /// </summary>>
        int BoardSizeX { get; set; }

        /// <summary>
        /// Gets or sets width of the board
        /// </summary>
        int BoardSizeY { get; set; }

        /// <summary>
        /// Draw the board
        /// </summary>
        void Draw();

        /// <summary>
        /// Draw some symbol on the board
        /// </summary>
        /// <param name="symbol">Char to draw</param>
        /// <param name="coordX">Coordinate from left</param>
        /// <param name="coordY">Coordinate from top</param>
        void DrawAt(char symbol, int coordX, int coordY);
    }
}