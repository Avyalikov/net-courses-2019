// <copyright file="Board.cs" company="IPavlov">
// Copyright (c) IPavlov. All rights reserved.
// </copyright>

namespace ConsoleDraw.Provider
{
    using ConsoleDraw.Interfaces;

    /// <summary>
    /// board provider class.
    /// </summary>
    internal class Board : IBoard
    {
        private readonly char angle = '+';
        private readonly char vertical = '|';
        private readonly char horizontal = '-';

        private readonly IInputOutputDevice iOProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="Board"/> class.
        /// </summary>
        /// <param name="iOProvider">IODevice.</param>
        public Board(IInputOutputDevice iOProvider)
        {
            this.iOProvider = iOProvider;
        }

        /// <inheritdoc/>
        /// X-axis size
        public int BoardSizeX { get; set; }

        /// <inheritdoc/>
        /// Y-axis size
        public int BoardSizeY { get; set; }

        /// <inheritdoc/>
        /// create board
        public void Create()
        {
            this.WriteAt(this.angle, 0, 0);
            this.WriteAt(this.angle, 0, this.BoardSizeY - 1);
            this.WriteAt(this.angle, this.BoardSizeX - 1, 0);
            this.WriteAt(this.angle, this.BoardSizeX - 1, this.BoardSizeY - 1);

            for (int i = 1; i < this.BoardSizeX - 1; i++)
            {
                this.WriteAt(this.horizontal, i, 0);
                this.WriteAt(this.horizontal, i, this.BoardSizeY - 1);
            }

            for (int i = 1; i < this.BoardSizeY - 1; i++)
            {
                this.WriteAt(this.vertical, 0, i);
                this.WriteAt(this.vertical, this.BoardSizeX - 1, i);
            }
        }

        /// <inheritdoc/>
        /// draw on board
        public void WriteAt(char c, int x, int y)
        {
            this.iOProvider.SetPosition(x, y);
            this.iOProvider.WriteOutput(c.ToString());
        }
    }
}
