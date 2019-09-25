// <copyright file="Board.cs" company=".net courses 2019">
// All rights reserved.
// </copyright>
// <author>Roman Zuev</author>

namespace HW3_console_draw_game
{
    /// <summary>
    /// Basic game logic class
    /// </summary>
    internal class Board : IBoard
    {
        /// <summary>
        /// Defines the boardSizeX
        /// </summary>
        private int boardSizeX;

        /// <summary>
        /// Defines the boardSizeY
        /// </summary>
        private int boardSizeY;

        /// <summary>
        /// Gets or sets the BoardSizeX
        /// </summary>
        public int BoardSizeX
        {
            get => this.boardSizeX;
            set
            {
                if (value < 4 || value > 50)
                {
                    this.boardSizeX = 50;
                }
                else
                {
                    this.boardSizeX = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the BoardSizeY
        /// </summary>
        public int BoardSizeY
        {
            get => this.boardSizeY;
            set
            {
                if (value < 4 || value > 20)
                {
                    this.boardSizeY = 20;
                }
                else
                {
                    this.boardSizeY = value;
                }
            }
        }
    }
}
