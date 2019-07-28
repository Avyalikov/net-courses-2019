using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleBoard b = new ConsoleBoard();
            GameMenu gm = new GameMenu(b);
            Game g = new Game(b, gm);
            g.Play();
        }
    }
}
