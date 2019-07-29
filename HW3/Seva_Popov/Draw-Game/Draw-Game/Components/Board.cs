namespace Draw_Game.Components
{
    using Draw_Game.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Text;

   public class Board : IBoard
    {
        private readonly string angle = "+";
        private readonly string vertical = "|";
        private readonly string horizontal = "-";

        //private int origRow;
        //private int origCol;

       

        public int SizeX { get; set; } = 40;

        public int SizeY { get; set; } = 20;
        public string ST { get; set; } 

        public void Create(IBoard board)
        {
            // Console.Clear();
            //this.origCol = Console.CursorLeft; 
            //this.origRow = Console.CursorTop;            

            // Draw the angles
            WriteAt(this.angle, 0, 0);
            WriteAt(this.angle, 0, this.SizeY - 1);
            WriteAt(this.angle, this.SizeX - 1, 0);
            WriteAt(this.angle, this.SizeX - 1, this.SizeY - 1);

            for (int i = 1; i < this.SizeX - 1; i++)
            {
                WriteAt(this.horizontal, i, 0);
                WriteAt(this.horizontal, i, this.SizeY - 1);                 
            }

            for (int i = 1; i < this.SizeY - 1; i++)
            {
                WriteAt(this.vertical, 0, i);
                WriteAt(this.vertical, this.SizeX - 1, i);  
            }

            WriteAt("\n", 0, this.SizeY);
        }

        public void Curve(IBoard board)
        {
            for (int i = 1; i < SizeX - 1; i++)
            {
                int func = SizeY - (int)Math.Pow(i, 2);
                if (func < 1)
                {
                    break;
                }

                WriteAt("*", i, func); // Draw the dot
                WriteAt("\n", 0, this.SizeY);
            }
        }

        public void HorizontalLine(IBoard board)
        {
            for (int i = 1; i < SizeX - 1; i++)
            {
                WriteAt("-", i, (SizeY / 2) + 2); // Draw the horizontal line, from left to right.                
            }
            WriteAt("\n", 0, this.SizeY);
        }

        public void SimpleDot(IBoard board)
        {
            WriteAt(".", SizeX / 2, SizeY / 2); // Draw the dot
            WriteAt("\n", 0, this.SizeY);
        }

        public void VerticalLine(IBoard board)
        {
            for (int i = 1; i < SizeY - 1; i++)
            {
                WriteAt("|", (SizeX / 2) + 2, i); // Draw the vertical line, from up to down
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
