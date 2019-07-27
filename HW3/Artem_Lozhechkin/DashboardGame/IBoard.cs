using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardGame
{
    interface IBoard
    {
        void DrawBoard();
        void DrawAtPosition(int x, int y, string s);
        int GetHeight();
        int GetWidth();
        void SetColor(ConsoleColor color);
        void ResetColor();
        string ReadLine();
        void Clear();
    }
}
