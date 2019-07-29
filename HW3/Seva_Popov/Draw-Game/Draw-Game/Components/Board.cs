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

        /*
        public DashBoard(int length, int width)
        {
            this.BoardSizeX = length;
            this.BoardSizeY = width;
        }
        */

        public int SizeX { get; set; } = 40;

        public int SizeY { get; set; } = 20;

        public void Create(CoundDel coundDel)
        {
            // Console.Clear();
            //this.origCol = Console.CursorLeft; 
            //this.origRow = Console.CursorTop;            

            // Draw the angles
            coundDel(this.angle, 0, 0);
            coundDel(this.angle, 0, this.SizeY - 1);
            coundDel(this.angle, this.SizeX - 1, 0);
            coundDel(this.angle, this.SizeX - 1, this.SizeY - 1);

            for (int i = 1; i < this.SizeX - 1; i++)
            {
                coundDel(this.horizontal, i, 0);         
                coundDel(this.horizontal, i, this.SizeY - 1);                 
            }

            for (int i = 1; i < this.SizeY - 1; i++)
            {
                coundDel(this.vertical, 0, i);           
                coundDel(this.vertical, this.SizeX - 1, i);  
            }

            coundDel("\n", 0, this.SizeY);
        }

        public void Curve(CoundDel coundDel)
        {
            for (int i = 1; i < SizeX - 1; i++)
            {
                int func = SizeY - (int)Math.Pow(i, 2);
                if (func < 1)
                {
                    break;
                }

                coundDel("*", i, func); // Draw the dot
                coundDel("\n", 0, this.SizeY);
            }
        }

        public void HorizontalLine(CoundDel coundDel)
        {
            for (int i = 1; i < SizeX - 1; i++)
            {
                coundDel("-", i, (SizeY / 2) + 2); // Draw the horizontal line, from left to right.                
            }
            coundDel("\n", 0, this.SizeY);
        }

        public void SimpleDot(CoundDel coundDel)
        {
            coundDel(".", SizeX / 2, SizeY / 2); // Draw the dot
            coundDel("\n", 0, this.SizeY);
        }

        public void VerticalLine(CoundDel coundDel)
        {
            for (int i = 1; i < SizeY - 1; i++)
            {
                coundDel("|", (SizeX / 2) + 2, i); // Draw the vertical line, from up to down
            }
            coundDel("\n", 0, this.SizeY);
        }
    }
}
