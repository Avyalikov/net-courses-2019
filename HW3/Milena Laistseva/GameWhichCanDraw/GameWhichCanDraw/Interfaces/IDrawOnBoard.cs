using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWhichCanDraw
{
    public interface IDrawOnBoard
    {
        void DrawSimpleDot(IBoard board);
        void DrawHorizontalLine(IBoard board);

        void DrawVerticalLine(IBoard board);

        void DrawCircle(IBoard board);

        void CleanBoard(IBoard board);
    }
}
