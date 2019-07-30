//-----------------------------------------------------------------------
// <copyright file="ConsoleBoard.cs" company="AVLozhechkin">
//     Copyright (c) AVLozhechkin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------   
namespace DashboardGame
{
    using System;
    using System.Text;

    /// <summary>
    /// This class implements IBoard using Console.
    /// </summary>
    internal class ConsoleBoard : IBoard
    {
        /// <summary>
        /// Initializes a new instance of the ConsoleBoard class.
        /// </summary>
        /// <param name="settings">Settings which is used for building Console window.</param>
        public ConsoleBoard(Settings settings)
        {
            this.BoardHeight = settings.BoardHeight;
            this.BoardWidth = settings.BoardWidth;
            Console.SetWindowSize(this.BoardWidth, this.BoardHeight);
            Console.BufferHeight = this.BoardHeight;
            Console.BufferWidth = this.BoardWidth;
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
            this.DefaultConsoleColor = Console.ForegroundColor;
        }

        /// <summary>
        /// Gets a board height.
        /// </summary>
        private int BoardHeight { get; }

        /// <summary>
        /// Gets a board width.
        /// </summary>
        private int BoardWidth { get; }

        /// <summary>
        /// Gets a default ConsoleColor of the Console.
        /// </summary>
        private ConsoleColor DefaultConsoleColor { get; }
        
        /// <summary>
        /// This method draws a border for the plotting area.
        /// </summary>
        public void DrawBoard()
        {
            this.SetColor(ConsoleColor.Green);
            for (int i = 0; i < this.BoardWidth - 2; i++)
            {
                this.DrawAtPosition((-this.BoardWidth / 2) + i, -((this.BoardHeight / 2) - 1), "―");
                this.DrawAtPosition((-this.BoardWidth / 2) + i, this.BoardHeight / 2, "―");
            }

            for (int i = 0; i < this.BoardHeight - 1; i++)
            {
                this.DrawAtPosition(-this.BoardWidth / 2, (this.BoardHeight / 2) - i, "|");
                this.DrawAtPosition((this.BoardWidth / 2) - 2, (this.BoardHeight / 2) - i, "|");
            }

            this.DrawAtPosition((this.BoardWidth / 2) - 2, (-this.BoardHeight / 2) + 1, "+");
            this.DrawAtPosition(-this.BoardWidth / 2, this.BoardHeight / 2, "+");
            this.DrawAtPosition(-this.BoardWidth / 2, (-this.BoardHeight / 2) + 1, "+");
            this.DrawAtPosition((this.BoardWidth / 2) - 2, this.BoardHeight / 2, "+");
            this.ResetColor();
        }

        /// <summary>
        /// Draws a string with setting position.
        /// </summary>
        /// <param name="x">X-coordinate for drawing.</param>
        /// <param name="y">Y-coordinate for drawing.</param>
        /// <param name="s">String to draw.</param>
        public void DrawAtPosition(int x, int y, string s)
        {
            Console.SetCursorPosition((this.BoardWidth / 2) + x, (this.BoardHeight / 2) - y);
            Console.Write(s);
        }

        /// <summary>
        /// This method waits for pressing a key on the ConsoleBoard.
        /// </summary>
        /// <returns>Info about pressed key.</returns>
        public ConsoleKeyInfo ReadKey() => Console.ReadKey(false);

        /// <summary>
        /// This method reads a line from ConsoleBoard.
        /// </summary>
        /// <returns>String read from console.</returns>
        public string ReadLine() => Console.ReadLine();

        /// <summary>
        /// This method is used to draw axis.
        /// </summary>
        public void DrawAxis()
        {
            this.SetColor(ConsoleColor.Green);
            for (int i = (-this.BoardHeight / 2) + 2; i < this.BoardHeight / 2; i++)
            {
                this.DrawAtPosition(0, i, "|");
            }

            for (int i = (-this.BoardWidth / 2) + 1; i < (this.BoardWidth / 2) - 2; i++)
            {
                this.DrawAtPosition(i, 0, "―");
            }

            this.DrawAtPosition(0, 0, "+");
            this.DrawAtPosition(1, 1, "0");
            this.ResetColor();
        }

        /// <summary>
        /// This method returns board's height.
        /// </summary>
        /// <returns>Height of the board.</returns>
        public int GetHeight()
        {
            return this.BoardHeight;
        }

        /// <summary>
        /// This method returns board's width.
        /// </summary>
        /// <returns>Width of the board.</returns>
        public int GetWidth()
        {
            return this.BoardWidth;
        }

        /// <summary>
        /// Sets color for drawing.
        /// </summary>
        /// <param name="color">Color to set.</param>
        public void SetColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        /// <summary>
        /// Resets drawing color to the default.
        /// </summary>
        public void ResetColor()
        {
            Console.ForegroundColor = this.DefaultConsoleColor;
        }

        /// <summary>
        /// Clears the board.
        /// </summary>
        public void Clear()
        {
            Console.Clear();
            this.DrawBoard();
        }
    }
}
