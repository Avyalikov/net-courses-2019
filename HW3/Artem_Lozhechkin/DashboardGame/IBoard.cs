//-----------------------------------------------------------------------
// <copyright file="IBoard.cs" company="AVLozhechkin">
//     Copyright (c) AVLozhechkin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------   
namespace DashboardGame
{
    using System;
    
    /// <summary>
    /// This interface is used for providing with an input-output for drawing game.
    /// </summary>
    internal interface IBoard
    {
        /// <summary>
        /// Gets a board height.
        /// </summary>
        int BoardSizeY { get; }

        /// <summary>
        /// Gets a board width.
        /// </summary>
        int BoardSizeX { get; }
        /// <summary>
        /// This method should draw a plotting area.
        /// </summary>
        void DrawBoard();

        /// <summary>
        /// This method should draw a string at given coordinates.
        /// </summary>
        /// <param name="x">X-coordinate for drawing.</param>
        /// <param name="y">Y-coordinate for drawing.</param>
        /// <param name="s">String for drawing.</param>
        void DrawAtPosition(int x, int y, string s);

        /// <summary>
        /// This method should draw an axis on the plotting area.
        /// </summary>
        void DrawAxis();

        /// <summary>
        /// This method should set color for drawing on the plotting area.
        /// </summary>
        /// <param name="color">Color for drawing.</param>
        void SetColor(ConsoleColor color);

        /// <summary>
        /// This method should reset drawing color to the default.
        /// </summary>
        void ResetColor();

        /// <summary>
        /// This method should read a string from the plotting area.
        /// </summary>
        /// <returns>String from the plotting area.</returns>
        string ReadLine();

        /// <summary>
        /// This method should clear the board.
        /// </summary>
        void Clear();

        /// <summary>
        /// This method should read the pressed key.
        /// </summary>
        /// <returns>Info about pressed key.</returns>
        ConsoleKeyInfo ReadKey();
    }
}
