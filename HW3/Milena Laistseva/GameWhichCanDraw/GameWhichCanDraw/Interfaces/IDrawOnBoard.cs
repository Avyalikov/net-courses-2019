// <copyright file="IDrawOnBoard.cs" company="Epam">
// Copyright (c) Epam. All rights reserved.
// </copyright>

namespace GameWhichCanDraw
{
    /// <summary>
    /// Draw on board interface
    /// </summary>
    public interface IDrawOnBoard
    {
        /// <summary>
        /// Draw simple dot method
        /// </summary>
        /// <param name="board">Board component</param>
        void DrawSimpleDot(IBoard board);

        /// <summary>
        /// Draw horizontal line method
        /// </summary>
        /// <param name="board">Board component</param>
        void DrawHorizontalLine(IBoard board);

        /// <summary>
        /// Draw vertical line method
        /// </summary>
        /// <param name="board">Board component</param>
        void DrawVerticalLine(IBoard board);

        /// <summary>
        /// Draw circle method
        /// </summary>
        /// <param name="board">Board component</param>
        void DrawCircle(IBoard board);

        /// <summary>
        /// Clean board method
        /// </summary>
        /// <param name="board">Board component</param>
        void CleanBoard(IBoard board);
    }
}
