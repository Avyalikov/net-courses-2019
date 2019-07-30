using System;
using System.Collections.Generic;
using System.Text;

namespace Console_Draw_Game.Interfaces
{
    interface IFigures
    {
        void Dot(IBoard board);

        void HorizontalLine(IBoard board);

        void VerticalLine(IBoard board);

        void SharpLine(IBoard board);
    }
}
