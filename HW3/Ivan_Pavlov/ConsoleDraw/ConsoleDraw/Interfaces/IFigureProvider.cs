// <copyright file="IFigureProvider.cs" company="IPavlov">
// Copyright (c) IPavlov. All rights reserved.
// </copyright>

namespace ConsoleDraw.Interfaces
{
    /// <summary>
    /// figure interface.
    /// </summary>
    public interface IFigureProvider
    {
        /// <summary>
        /// dot.
        /// </summary>
        /// <param name="board">draw simple dot.</param>
        void Dot(IBoard board);

        /// <summary>
        /// vertical line.
        /// </summary>
        /// <param name="board">draw vertical line.</param>
        void VerticalLine(IBoard board);

        /// <summary>
        /// horizontal line.
        /// </summary>
        /// <param name="board">draw horizontal line.</param>
        void HorizontalLine(IBoard board);

        /// <summary>
        /// curve.
        /// </summary>
        /// <param name="board">draw curve.</param>
        void Curve(IBoard board);
    }
}
