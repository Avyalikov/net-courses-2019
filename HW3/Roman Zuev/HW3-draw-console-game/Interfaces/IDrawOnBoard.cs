// <copyright file="IDrawOnBoard.cs" company=".net courses 2019">
// All rights reserved.
// </copyright>
// <author>Roman Zuev</author>

namespace HW3_console_draw_game
{
    /// <summary>
    /// Defines the <see cref="IDrawOnBoard" />
    /// </summary>
    internal interface IDrawOnBoard
    {
        /// <summary>
        /// The DrawBoard
        /// </summary>
        /// <param name="board">The board<see cref="IBoard"/></param>
        void DrawBoard(IBoard board);

        /// <summary>
        /// The DrawSimpleDot
        /// </summary>
        /// <param name="board">The board<see cref="IBoard"/></param>
        void DrawSimpleDot(IBoard board);

        /// <summary>
        /// The DrawVerticalLine
        /// </summary>
        /// <param name="board">The board<see cref="IBoard"/></param>
        void DrawVerticalLine(IBoard board);

        /// <summary>
        /// The DrawHorizontalLine
        /// </summary>
        /// <param name="board">The board<see cref="IBoard"/></param>
        void DrawHorizontalLine(IBoard board);

        /// <summary>
        /// The DrawV
        /// </summary>
        /// <param name="board">The board<see cref="IBoard"/></param>
        void DrawV(IBoard board);

        /// <summary>
        /// The ClearConsole
        /// </summary>
        /// <param name="board">The board<see cref="IBoard"/></param>
        void ClearConsole(IBoard board);
    }
}
