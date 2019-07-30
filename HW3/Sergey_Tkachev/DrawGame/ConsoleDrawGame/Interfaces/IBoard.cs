using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDrawGame.Interfaces
{
    interface IBoard
    {
        /// <summary>Prints board.</summary>
        void PrintBoard();
        /// <summary>Prints simple dot.</summary>
        void PrintDot();
        /// <summary>Prints horizontal line.</summary>
        void PrintHorizontal();
        /// <summary>Prints Vertical line.</summary>
        void PrintVertical();
        /// <summary>Prints Other Curve.</summary>
        void PrintOtherCurve();
        int boardSizeX { get; set; }
        int boardSizeY { get; set; }
    }
}
