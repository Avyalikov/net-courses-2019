using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDrawGame.Interfaces
{
    interface IBoard
    {
        void PrintBoard();
        void PrintDot();
        void PrintHorizontal();
        void PrintVertical();
        void PrintOtherCurve();
    }
}
