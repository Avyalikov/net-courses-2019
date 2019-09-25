//-----------------------------------------------------------------------
// <copyright file="FigureComponent.cs" company="Epam">
//     Copyright (c) Epam. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Components
{
    using Interfaces;

    /// <summary>
    /// Class for draw figures
    /// </summary>
    public class FigureComponent : IFigureProvider
    {
        /// <summary>
        /// Draw a dot
        /// </summary>
        /// <param name="board">Drawing screen</param>
        public void Dot(IBoard board)
        {
            board.DrawAt('*', board.BoardSizeX / 2, board.BoardSizeY / 2);
        }

        /// <summary>
        /// Draw a vertical line
        /// </summary>
        /// <param name="board">Drawing screen</param>
        public void VerticalLine(IBoard board)
        {
            for (int i = 2; i < board.BoardSizeY - 1; i++)
            {
                board.DrawAt('|', 1, i);
            }
        }

        /// <summary>
        /// Draw a horizontal line
        /// </summary>
        /// <param name="board">Drawing screen</param>
        public void HorizontalLine(IBoard board)
        {
            for (int i = 2; i < board.BoardSizeX - 1; i++)
            {
                board.DrawAt('-', i, 1);
            }
        }

        /// <summary>
        /// Draw a rectangle
        /// </summary>
        /// <param name="board">Drawing screen</param>
        public void Rectangle(IBoard board)
        {
            board.DrawAt('+', (board.BoardSizeX / 2) + 1, (board.BoardSizeY / 2) + 1);
            board.DrawAt('+', board.BoardSizeX - 1, (board.BoardSizeY / 2) + 1);
            board.DrawAt('+', (board.BoardSizeX / 2) + 1, board.BoardSizeY - 1);
            board.DrawAt('+', board.BoardSizeX - 1, board.BoardSizeY - 1);
            for (int i = (board.BoardSizeX / 2) + 2; i < board.BoardSizeX - 1; i++)
            {
                board.DrawAt('-', i, (board.BoardSizeY / 2) + 1);
                board.DrawAt('-', i, board.BoardSizeY - 1);
            }

            for (int i = (board.BoardSizeY / 2) + 2; i < board.BoardSizeY - 1; i++)
            {
                board.DrawAt('|', (board.BoardSizeX / 2) + 1, i);
                board.DrawAt('|', board.BoardSizeX - 1, i);
            }
        }
    }
}
