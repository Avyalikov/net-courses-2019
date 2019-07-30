namespace HW3
{
    using System;

    public class Board : IBoard
    {
        public Board(int boardSizeX, int boardSizeY)
        {
            if (boardSizeX < 10)
            {
                boardSizeX = 10;
            }

            if (boardSizeX >= Console.BufferWidth)
            {
                boardSizeX = Console.BufferWidth - 2;
            }

            if (boardSizeY < 10)
            {
                boardSizeY = 10;
            }

            if (boardSizeX >= Console.BufferHeight)
            {
                boardSizeY = Console.BufferHeight - 2;
            }

            this.BoardSizeX = boardSizeX;
            this.BoardSizeY = boardSizeY;
        }

        public int BoardSizeX { get; set; }

        public int BoardSizeY { get; set; }
    }
}