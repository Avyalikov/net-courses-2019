//-----------------------------------------------------------------------
// <copyright file="Board.cs" company="EPAM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace The_draw_game.Components
{
    using System;
    using DataTypes;
    using Interfaces;

    /// <summary>
    /// IBoard component
    /// </summary>
    public class Board : IBoard
    {
        /// <summary>
        /// IInputOutput field
        /// </summary>
        private readonly IInputOutput io;

        /// <summary>
        /// borderline field
        /// </summary>
        private Rectangle borderline;

        /// <summary>
        /// Board stile
        /// </summary>
        private string boardStile;

        /// <summary>
        /// Board width
        /// </summary>
        private int _boardSizeX;

        /// <summary>
        /// Board height
        /// </summary>
        private int _boardSizeY;

        /// <summary>
        /// Initializes a new instance of the <see cref="Board" /> class
        /// </summary>
        /// <param name="io">Input/Output component</param>
        /// <param name="width">Board width</param>
        /// <param name="height">Board height</param>
        /// <param name="boardStile">Board stile provider component</param>
        public Board(IInputOutput io, int width, int height, string boardStile)
        {
            this.io = io;
            try
            {
                io.SetWindowSize(width + 1, height + 10);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            this.boardSizeX = width;
            this.boardSizeY = height;
            this.boardStile = boardStile;
        }

        /// <summary>
        /// Gets or sets board width
        /// </summary>
        public int boardSizeX
        {
            get
            {
                return this._boardSizeX;
            }

            set
            {
                if (value < 30)
                {
                    throw new Exception("Window width is very low");
                }

                this._boardSizeX = value;
            }
        }

        /// <summary>
        /// Gets or sets board height
        /// </summary>
        public int boardSizeY
        {
            get
            {
                return this._boardSizeY;
            }

            set
            {
                if (value < 15)
                {
                    throw new Exception("Window height is very low");
                }

                this._boardSizeY = value;
            }
        }

        /// <summary>
        ///  Draw figure
        /// </summary>
        /// <param name="board">Board component</param>
        public void Draw(IBoard board)
        {            
            if (this.borderline == null)
            {
                this.borderline = new Rectangle(
                    new Point(0, 0),
                    new Point(board.boardSizeX, board.boardSizeY));
            }

            foreach (var point in this.borderline.Body)
            {
                this.io.Print(point, this.boardStile);
            }
        }
    }
}