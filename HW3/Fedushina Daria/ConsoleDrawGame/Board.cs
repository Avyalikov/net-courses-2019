using System;

namespace ConsoleDrawGame
{
    class Board : IBoard
    {

        private readonly IInputOutputDevice ioDevice;
        public Board(IInputOutputDevice ioDevice)
        {
            this.ioDevice = ioDevice ?? new ConsoleIO();
        }
        
        public int FigOX { get; set; }                      // get the adress of origin dot of the figure container
        public int FigOY { get; set; }                      // get the adress of origin dot of the figure container
        public int OX
        {
            get { return ioDevice.GetCursorPosition().Item1; }   // get the adress of origin dot of the board
        }                                                                   
        public int OY
        {
            get { return ioDevice.GetCursorPosition().Item2; } // get the adress of origin dot of the board;
        }
        public void WriteAt(string s, int x, int y)
        {
            try
            {
                ioDevice.SetCursorPosition(FigOX + x, FigOY + y);
                ioDevice.WriteWithStayOnLine(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                ioDevice.Clear();
                ioDevice.WriteOutput(e.Message);
            }
        }

        private int Horizontal;
        private int Vertical;

        public int boardSizeX
        {
            get { return Horizontal; }
            set
            {
                if (value < 2 || value > 70)
                    Horizontal = 70;
                else
                    Horizontal = value;
            }
        }

        public int boardSizeY
        {
            get { return Vertical; }
            set
            {
                if (value < 2 || value > 20)
                    Vertical = 20;
                else
                    Vertical = value;
            }
        }



        public void DrawBoard(IBoard board)
        {
            FigOX = OX;
            FigOY = OY+1;
            WriteAt("+", 0, 0);
            // Draw the left side of a 5x5 rectangle, from top to bottom.
            for (int i = 1; i < boardSizeY; i++)
            {
                WriteAt("|", 0, i);
            }
            WriteAt("+", 0, boardSizeY);


            // Draw the bottom side, from left to right.
            for (int i = 1; i < boardSizeX; i++)
            {
                WriteAt("-", i, boardSizeY);
            }
            WriteAt("+", boardSizeX, boardSizeY);

            // Draw the right side, from bottom to top.
            for (int i = boardSizeY - 1; i > 0; i--)
            {
                WriteAt("|", boardSizeX, i);
            }
            WriteAt("+", boardSizeX, 0);

            // Draw the top side, from right to left.
            for (int i = boardSizeX - 1; i > 0; i--)
            {
                WriteAt("-", i, 0);
            }

        }
    }
}

