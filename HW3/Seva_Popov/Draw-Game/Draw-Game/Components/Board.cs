namespace Draw_Game.Components
{
    using Draw_Game.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Text;

   public class Board : IBoard
    {
        private readonly string plus = "+";
        private readonly string line = "|";
        private readonly string dash = "-";

        public int SizeX { get; set; }
        public int SizeY { get; set; }

        public void Create(IBoard board)
        {
            WriteAt(this.plus, 0, 0);
            WriteAt(this.plus, 0, this.SizeY - 1);
            WriteAt(this.plus, this.SizeX - 1, 0);
            WriteAt(this.plus, this.SizeX - 1, this.SizeY - 1);

            for (int i = 1; i < this.SizeX - 1; i++)
            {
                WriteAt(this.dash, i, 0);
                WriteAt(this.dash, i, this.SizeY - 1);                 
            }

            for (int i = 1; i < this.SizeY - 1; i++)
            {
                WriteAt(this.line, 0, i);
                WriteAt(this.line, this.SizeX - 1, i);  
            }

            WriteAt("\n", 0, this.SizeY);
        }

        public void AnotherShape(IBoard board)
        {
            for (int i = 1; i < SizeX - 1; i++)
            {
                WriteAt("$", i, (SizeY / 4) + 2);
            }
            for (int i = 1; i < SizeY - 1; i++)
            {
                WriteAt("$", (SizeX / 4) + 2, i);
            }
        }

        public void HorizontalLine(IBoard board)
        {
            for (int i = 1; i < SizeX - 1; i++)
            {
                WriteAt("-", i, (SizeY / 2) + 2);            
            }
            WriteAt("\n", 0, this.SizeY);
        }

        public void SimpleDot(IBoard board)
        {
            WriteAt(".", SizeX / 2, SizeY / 2);
            WriteAt("\n", 0, this.SizeY);
        }

        public void VerticalLine(IBoard board)
        {
            for (int i = 1; i < SizeY - 1; i++)
            {
                WriteAt("|", (SizeX / 2) + 2, i);
            }
            WriteAt("\n", 0, this.SizeY);
        }

        public static int origRow;
        public static int origCol;

        public void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
    }
}
