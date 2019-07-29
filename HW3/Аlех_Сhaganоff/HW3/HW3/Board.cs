namespace HW3
{
    using System;
    internal class Board : IBoard
    {
        public int BoardSizeX {get; set;}

        public int BoardSizeY {get; set;}

        public Board (int BoardSizeX, int BoardSizeY)
        {
            if(BoardSizeX<10)
            {
                BoardSizeX = 10;
            }

            if (BoardSizeX >= Console.BufferWidth)
            {
                BoardSizeX = Console.BufferWidth - 2;
            }

            if (BoardSizeY<10)
            {
                BoardSizeY = 10;
            }

            if (BoardSizeY >= Console.BufferHeight)
            {
                BoardSizeY = Console.BufferHeight - 2;
            }

            this.BoardSizeX = BoardSizeX;
            this.BoardSizeY = BoardSizeY;
        }
    }
}