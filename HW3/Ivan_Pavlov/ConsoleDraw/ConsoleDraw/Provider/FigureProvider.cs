// <copyright file="FigureProvider.cs" company="IPavlov">
// Copyright (c) IPavlov. All rights reserved.
// </copyright>

namespace ConsoleDraw.Provider
{
    using System;
    using ConsoleDraw.Interfaces;

    /// <summary>
    /// figure provoder class.
    /// </summary>
    internal class FigureProvider : IFigureProvider
    {
        /// <inheritdoc/>
        /// draw curve
        public void Curve(IBoard board)
        {
            for (int i = 0; i < board.BoardSizeX - 1; i++)
            {
                int func;
                if ((func = board.BoardSizeY - (int)Math.Pow(i, 1)) < 1)
                {
                    break;
                }

                board.WriteAt('*', i, func);
            }
        }

        /// <inheritdoc/>
        /// draw horizontal line
        public void HorizontalLine(IBoard board)
        {
            for (int i = 1; i < board.BoardSizeX - 1; i++)
            {
                board.WriteAt('-', i, board.BoardSizeY / 2);
            }
        }

        /// <inheritdoc/>
        /// draw dor
        public void Dot(IBoard board)
        {
            board.WriteAt('.', (board.BoardSizeX / 2) - 2, (board.BoardSizeY / 2) - 1);
        }

        /// <inheritdoc/>
        /// draw verttical line
        public void VerticalLine(IBoard board)
        {
            for (int i = 1; i < board.BoardSizeY - 1; i++)
            {
                board.WriteAt('|', (board.BoardSizeX / 2) + 1, i);
            }
        }
    }
}
