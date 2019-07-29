//-----------------------------------------------------------------------
// <copyright file="IFigureProvider.cs" company="Epam">
//     Copyright (c) Epam. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for draw figures
    /// </summary>
    public interface IFigureProvider
    {
        /// <summary>
        /// Draw a dot
        /// </summary>
        /// <param name="board">Drawing screen</param>
        void Dot(IBoard board);

        /// <summary>
        /// Draw a vertical line
        /// </summary>
        /// <param name="board">Drawing screen</param>
        void VerticalLine(IBoard board);

        /// <summary>
        /// Draw a horizontal line
        /// </summary>
        /// <param name="board">Drawing screen</param>
        void HorizontalLine(IBoard board);

        /// <summary>
        /// Draw a rectangle
        /// </summary>
        /// <param name="board">Drawing screen</param>
        void Rectangle(IBoard board);
    }
}
