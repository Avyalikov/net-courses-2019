// <copyright file="IBoard.cs" company="IPavlov">
// Copyright (c) IPavlov. All rights reserved.
// </copyright>

namespace ConsoleDraw.Interfaces
{
    /// <summary>
    /// board interface.
    /// </summary>
    public interface IBoard
    {
        /// <summary>
        /// Gets or sets x-coordinate.
        /// </summary>
        int BoardSizeX { get; set; }

        /// <summary>
        /// Gets or sets y-coordinate.
        /// </summary>
        int BoardSizeY { get; set; }

        /// <summary>
        /// Create board.
        /// </summary>
        void Create();

        /// <summary>
        /// Write on board.
        /// </summary>
        /// <param name="c">char.</param>
        /// <param name="x">x-coordinate.</param>
        /// <param name="y">y-coordinate.</param>
        void WriteAt(char c, int x, int y);
    }
}
