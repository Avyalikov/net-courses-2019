namespace Draw_Game.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;

   public interface IBoard
    {
         void Create(CoundDel coundDel);
         void Curve(CoundDel coundDel);
         void HorizontalLine(CoundDel coundDel);
         void SimpleDot(CoundDel coundDel);
         void VerticalLine(CoundDel coundDel);
    }
}
