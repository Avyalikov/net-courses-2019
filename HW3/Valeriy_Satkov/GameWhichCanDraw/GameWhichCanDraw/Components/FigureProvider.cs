// <copyright file="FigureProvider.cs" company="Valeriy Satkov">
// All rights reserved.
// </copyright>
// <author>Valeriy Satkov</author>

namespace GameWhichCanDraw.Components
{
    using System;
    using Interfaces;

    /// <summary>
    /// Class with figures
    /// </summary>
    internal class FigureProvider : IFigureProvider
    {
        /// <summary>
        /// Draw curve on board
        /// </summary>
        /// <param name="board">The board for drawing<see cref="IBoard"/></param>
        public void Curve(IBoard board)
        {
            for (int i = 1; i < board.BoardSizeX - 1; i++)
            {
                int func = board.BoardSizeY - (int)Math.Pow(i, 2);
                if (func < 1)
                {
                    break;
                }

                board.WriteAt('*', i, func); // Draw the dot
            }
        }

        /// <summary>
        /// Draw horizontal line on board
        /// </summary>
        /// <param name="board">The board for drawing<see cref="IBoard"/></param>
        public void HorizontalLine(IBoard board)
        {
            for (int i = 1; i < board.BoardSizeX - 1; i++)
            {
                board.WriteAt('-', i, (board.BoardSizeY / 2) + 2); // Draw the horizontal line, from left to right.                
            }
        }

        /// <summary>
        /// Draw dot on board
        /// </summary>
        /// <param name="board">The board for drawing<see cref="IBoard"/></param>
        public void SimpleDot(IBoard board)
        {
            board.WriteAt('.', board.BoardSizeX / 2, board.BoardSizeY / 2); // Draw the dot
        }

        /// <summary>
        /// Draw vertical line on board
        /// </summary>
        /// <param name="board">The board for drawing<see cref="IBoard"/></param>
        public void VerticalLine(IBoard board)
        {
            for (int i = 1; i < board.BoardSizeY - 1; i++)
            {
                board.WriteAt('|', (board.BoardSizeX / 2) + 2, i); // Draw the vertical line, from up to down
            }
        }
    }
}
