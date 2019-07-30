// <copyright file="IFigureProvider.cs" company="Valeriy Satkov">
// All rights reserved.
// </copyright>
// <author>Valeriy Satkov</author>

namespace GameWhichCanDraw.Interfaces
{
    using System;
    using Interfaces;

    /// <summary>
    /// FiguresProvider interface
    /// </summary>
    public interface IFigureProvider
    {
        /// <summary>
        /// Draw dot on board
        /// </summary>
        /// <param name="board">The board for drawing<see cref="IBoard"/></param>
        void SimpleDot(IBoard board);

        /// <summary>
        /// Draw vertical line on board
        /// </summary>
        /// <param name="board">The board for drawing<see cref="IBoard"/></param>
        void VerticalLine(IBoard board);

        /// <summary>
        /// Draw horizontal line on board
        /// </summary>
        /// <param name="board">The board for drawing<see cref="IBoard"/></param>
        void HorizontalLine(IBoard board);

        /// <summary>
        /// Draw curve on board
        /// </summary>
        /// <param name="board">The board for drawing<see cref="IBoard"/></param>
        void Curve(IBoard board);
    }
}
