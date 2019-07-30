// <copyright file="IBoard.cs" company=".net courses 2019">
// All rights reserved.
// </copyright>
// <author>Roman Zuev</author>

namespace HW3_console_draw_game
{
    /// <summary>
    /// Defines the <see cref="IBoard" />
    /// </summary>
    internal interface IBoard
    {
        /// <summary>
        /// Gets or sets the BoardSizeX
        /// </summary>
        int BoardSizeX { get; set; }

        /// <summary>
        /// Gets or sets the BoardSizeY
        /// </summary>
        int BoardSizeY { get; set; }
    }
}
