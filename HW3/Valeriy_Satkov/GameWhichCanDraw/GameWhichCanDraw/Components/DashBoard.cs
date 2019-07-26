namespace GameWhichCanDraw.Components
{
    using System;

    internal class DashBoard : Interfaces.IBoard
    {
        private readonly char angle = '+';
        private readonly char vertical = '|';
        private readonly char horizontal = '-';

        private int origRow;
        private int origCol;

        /*
        public DashBoard(int length, int width)
        {
            this.BoardSizeX = length;
            this.BoardSizeY = width;
        }
        */

        public int BoardSizeX { get; set; }

        public int BoardSizeY { get; set; }        
        
        public virtual void Create()
        {
            // Console.Clear();
            this.origCol = Console.CursorLeft; // save original left coordinate
            this.origRow = Console.CursorTop; // save original top coordinate            

            // Draw the angles
            this.WriteAt(this.angle, 0, 0);
            this.WriteAt(this.angle, 0, this.BoardSizeY - 1);
            this.WriteAt(this.angle, this.BoardSizeX - 1, 0);
            this.WriteAt(this.angle, this.BoardSizeX - 1, this.BoardSizeY - 1);

            for (int i = 1; i < this.BoardSizeX - 1; i++)
            {
                this.WriteAt(this.horizontal, i, 0);         // Draw the top side, from right to left.
                this.WriteAt(this.horizontal, i, this.BoardSizeY - 1); // Draw the bottom side, from left to right.                
            }

            for (int i = 1; i < this.BoardSizeY - 1; i++)
            {
                this.WriteAt(this.vertical, 0, i);           // Draw the left side of a 5x5 rectangle, from top to bottom.
                this.WriteAt(this.vertical, this.BoardSizeX - 1, i);  // Draw the right side, from bottom to top.
            }

            // this.WriteAt('\r', 0, this.BoardSizeY);
        }

        public virtual void WriteAt(char c, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(this.origCol + x, this.origRow + y);
                Console.Write(c);
            }
            catch (ArgumentException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
    }
}
