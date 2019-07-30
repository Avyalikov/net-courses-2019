// <copyright file="IBoard.cs" company="Epam">
// Copyright (c) Epam. All rights reserved.
// </copyright>

namespace GameWhichCanDraw
{
    /// <summary>
    /// Board interface
    /// </summary>
    public interface IBoard
    {
        /// <summary>
        /// Gets or sets the board length
        /// </summary>
        int boardSizeX { get; set; }

        /// <summary>
        /// Gets or sets the board width
        /// </summary>
        int boardSizeY { get; set; }

        /// <summary>
        /// Write symbol using x and y coordinates
        /// </summary>
        /// /// <param name="s">The symbol</param>
        /// <param name="x">The x coordinate</param>
        /// <param name="y">The y Coordinate</param>
        void WriteAt(string s, int x, int y);

        /// <summary>
        /// Draw board method
        /// </summary>
        /// <param name="board">Board component</param>
        void DrawBoard(IBoard board);
    }
}
