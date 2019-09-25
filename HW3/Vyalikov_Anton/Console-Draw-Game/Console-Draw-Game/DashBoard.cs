namespace Console_Draw_Game
{
    using System;
    class DashBoard : Interfaces.IBoard
    {
        private readonly char angle = '+';
        private readonly char vertical = '|';
        private readonly char horizontal = '-';

        private int horizCoord;
        private int vertCoord;

        public int BoardSizeX { get; set; }

        public int BoardSizeY { get; set; }

        public virtual void Create()
        {
            this.vertCoord = Console.CursorLeft; // save vertical coordinate
            this.horizCoord = Console.CursorTop; // save horizontal coordinate            

            // Draw the angles
            this.Draw(this.angle, 0, 0);
            this.Draw(this.angle, 0, this.BoardSizeY - 1);
            this.Draw(this.angle, this.BoardSizeX - 1, 0);
            this.Draw(this.angle, this.BoardSizeX - 1, this.BoardSizeY - 1);

            for (int i = 1; i < this.BoardSizeX - 1; i++)
            {
                this.Draw(this.horizontal, i, 0);         // Draw the top side, from right to left.
                this.Draw(this.horizontal, i, this.BoardSizeY - 1); // Draw the bottom side, from left to right.                
            }

            for (int i = 1; i < this.BoardSizeY - 1; i++)
            {
                this.Draw(this.vertical, 0, i);           // Draw the left side of a rectangle, from top to bottom.
                this.Draw(this.vertical, this.BoardSizeX - 1, i);  // Draw the right side of a rectangle, from bottom to top.
            }
        }

        public virtual void Draw(char type, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(this.horizCoord + x, this.vertCoord + y);
                Console.Write(type);
            }

            catch (ArgumentException exception)
            {
                Console.Clear();
                Console.WriteLine(exception.Message);
            }
        }
    }
}
