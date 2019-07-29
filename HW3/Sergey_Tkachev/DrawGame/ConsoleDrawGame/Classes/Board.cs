using ConsoleDrawGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDrawGame.Classes
{
    class Board : IBoard
    {
        private readonly ConsoleInputOutput cio;

        public Board(IInputOutput concoleInputOutput)
        {
            this.cio = (ConsoleInputOutput) concoleInputOutput;
        }
        public void PrintBoard()
        {
            // Draw the left side of a 5x5 rectangle, from top to bottom.
            cio.WriteAt("+", 0, 0);
            cio.WriteAt("|", 0, 1);
            cio.WriteAt("|", 0, 2);
            cio.WriteAt("|", 0, 3);
            cio.WriteAt("+", 0, 4);

            // Draw the bottom side, from left to right.
            cio.WriteAt("-", 1, 4); // shortcut: WriteAt("---", 1, 4)
            cio.WriteAt("-", 2, 4); // ...
            cio.WriteAt("-", 3, 4); // ...
            cio.WriteAt("+", 4, 4);

            // Draw the right side, from bottom to top.
            cio.WriteAt("|", 4, 3);
            cio.WriteAt("|", 4, 2);
            cio.WriteAt("|", 4, 1);
            cio.WriteAt("+", 4, 0);

            // Draw the top side, from right to left.
            cio.WriteAt("-", 3, 0); // shortcut: WriteAt("---", 1, 0)
            cio.WriteAt("-", 2, 0); // ...
            cio.WriteAt("-", 1, 0); // ...
            //
        }

        public void PrintDot()
        {
            throw new NotImplementedException();
        }

        public void PrintHorizontal()
        {
            throw new NotImplementedException();
        }

        public void PrintOtherCurve()
        {
            throw new NotImplementedException();
        }

        public void PrintVertical()
        {
            throw new NotImplementedException();
        }
    }
}
