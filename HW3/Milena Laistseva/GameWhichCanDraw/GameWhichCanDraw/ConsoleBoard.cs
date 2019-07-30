// <copyright file="ConsoleBoard.cs" company="Epam">
// Copyright (c) Epam. All rights reserved.
// </copyright>

namespace GameWhichCanDraw
{
    using System;

    /// <summary>
    /// Console board
    /// </summary>
    public class ConsoleBoard : IBoard
    {
        /// <summary>
        /// Board length
        /// </summary>
        private int sizeX;

        /// <summary>
        /// Board width
        /// </summary>
        private int sizeY;

        /// <summary>
        /// Gets or sets board length
        /// </summary>
        public int boardSizeX
        {
            get
            {
                return this.sizeX;
            }

            set
            {
                if (value < 2 || value > 60)
                {
                    this.sizeX = 60;
                }
                else
                {
                    this.sizeX = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets board width
        /// </summary>
        public int boardSizeY
        {
            get
            {
                return this.sizeY;
            }

            set
            {
                if (value < 2 || value > 20)
                {
                    this.sizeY = 20;
                }
                else
                {
                    this.sizeY = value;
                }
            }
        }

        /// <summary>
        /// WriteAt component in console
        /// </summary>
        /// <param name="s">The console symbol</param>
        /// <param name="x">The x coordinate</param>
        /// <param name="y">The y coordinate</param>
        public void WriteAt (string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(x, y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Console draw board method
        /// </summary>
        /// <param name="board">Board component</param>
        public void DrawBoard(IBoard board)
        {
            this.WriteAt("+", 0, 0);
            for (int i = 1; i < this.boardSizeY; i++)
            {
                this.WriteAt("|", 0, i);
            }

            this.WriteAt("+", 0, this.boardSizeY);

            for(int i = 1; i < this.boardSizeX; i++)
            {
                this.WriteAt("-", i, this.boardSizeY);
            }

            this.WriteAt("+", this.boardSizeX, this.boardSizeY);

            for (int i = this.boardSizeY - 1; i > 0; i--)
            {
                this.WriteAt("|", this.boardSizeX, i);
            }

            this.WriteAt("+", this.boardSizeX, 0);

            for (int i = this.boardSizeX - 1; i > 0; i--)
            {
                this.WriteAt("-", i, 0);
            }
        }        
    }
}
