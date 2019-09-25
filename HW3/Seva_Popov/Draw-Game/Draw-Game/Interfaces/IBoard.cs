namespace Draw_Game.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;

   public interface IBoard
    {
         int SizeX { get; set; }
         int SizeY { get; set; }
         void WriteAt(string s, int x, int y);
         void Create(IBoard board);
         void AnotherShape(IBoard board);
         void HorizontalLine(IBoard board);
         void SimpleDot(IBoard board);
         void VerticalLine(IBoard board);
    }
}
